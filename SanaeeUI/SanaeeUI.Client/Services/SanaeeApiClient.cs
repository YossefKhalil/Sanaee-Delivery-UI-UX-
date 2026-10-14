using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using SanaeeUI.Client.Models;

namespace SanaeeUI.Client.Services;

// Live implementation. It talks to the real Sanaee-Delivery API endpoints
// (all read/query endpoints plus the mutation endpoints that the front-end uses).
// If the API is unreachable the call fails gracefully and the UI surfaces LastError.
public class SanaeeApiClient : ISanaeeDataService
{
    private readonly HttpClient _http;

    private static readonly JsonSerializerOptions Json = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public SanaeeApiClient(HttpClient http) => _http = http;

    public DataMode Mode => DataMode.Live;
    public string? LastError { get; private set; }

    private async Task<IReadOnlyList<T>> GetListAsync<T>(string url)
    {
        try
        {
            LastError = null;
            var result = await _http.GetFromJsonAsync<List<T>>(url, Json);
            return result ?? new List<T>();
        }
        catch (Exception ex)
        {
            LastError = ex.Message;
            return new List<T>();
        }
    }

    private async Task<T?> GetItemAsync<T>(string url)
    {
        try
        {
            LastError = null;
            return await _http.GetFromJsonAsync<T>(url, Json);
        }
        catch (Exception ex)
        {
            LastError = ex.Message;
            return default;
        }
    }

    private async Task SendAsync(Func<Task> call)
    {
        try
        {
            LastError = null;
            await call();
        }
        catch (Exception ex)
        {
            LastError = ex.Message;
        }
    }

    public async Task<DashboardStats> GetDashboardStatsAsync()
    {
        var orders = await GetOrdersAsync();
        var providers = await GetProvidersAsync();
        var documents = await GetDocumentsAsync();
        var customers = await GetCustomersAsync();
        var payments = await GetPaymentsAsync();
        var wallets = await GetWalletsAsync();
        var reviews = await GetReviewsAsync();

        var stats = new DashboardStats
        {
            TotalOrders = orders.Count,
            OpenOrders = orders.Count(o => o.Status == OrderStatus.Pending),
            InProgressOrders = orders.Count(o => o.Status == OrderStatus.InProgress),
            CompletedOrders = orders.Count(o => o.Status == OrderStatus.Completed),
            CancelledOrders = orders.Count(o => o.Status == OrderStatus.Cancelled),
            TotalProviders = providers.Count,
            OnlineProviders = providers.Count(p => p.IsAvailable),
            PendingDocuments = documents.Count(d => d.Status == DocumentStatus.Pending),
            TotalCustomers = customers.Count,
            TotalRevenue = payments.Where(p => p.Status == PaymentStatus.Success).Sum(p => p.Amount),
            WalletLiability = wallets.Sum(w => w.Balance),
            AverageRating = reviews.Count == 0 ? 0 : Math.Round(reviews.Average(r => r.Rating), 2)
        };
        stats.OrdersByStatus = Enum.GetValues<OrderStatus>()
            .Select(s => new OrderStatusBucket { Status = s, Count = orders.Count(o => o.Status == s) })
            .ToList();
        stats.OrdersByCategory = orders.GroupBy(o => o.Category)
            .Select(g => new CategoryBucket { Category = g.Key, Count = g.Count() })
            .OrderByDescending(c => c.Count).ToList();
        return stats;
    }

    public Task<IReadOnlyList<OrderListItem>> GetOrdersAsync() => GetListAsync<OrderListItem>("api/Orders/admin");
    public Task<OrderListItem?> GetOrderAsync(int id) => GetItemAsync<OrderListItem>($"api/Orders/{id}");
    public Task<IReadOnlyList<OfferListItem>> GetOffersAsync() => GetListAsync<OfferListItem>("api/Offers");
    public Task<IReadOnlyList<ProviderListItem>> GetProvidersAsync() => GetListAsync<ProviderListItem>("api/Providers");
    public Task<IReadOnlyList<CustomerListItem>> GetCustomersAsync() => GetListAsync<CustomerListItem>("api/Customers/admin");
    public Task<IReadOnlyList<CategoryListItem>> GetCategoriesAsync() => GetListAsync<CategoryListItem>("api/Categories");
    public Task<IReadOnlyList<CancelReasonListItem>> GetCancelReasonsAsync() => GetListAsync<CancelReasonListItem>("api/CancelationReason");
    public Task<IReadOnlyList<ProviderDocumentListItem>> GetDocumentsAsync() => GetListAsync<ProviderDocumentListItem>("api/ProviderDocuments");
    public Task<IReadOnlyList<WalletListItem>> GetWalletsAsync() => GetListAsync<WalletListItem>("api/Wallets/Admin");
    public Task<IReadOnlyList<WalletTransactionListItem>> GetTransactionsAsync() => GetListAsync<WalletTransactionListItem>("api/WalletTransaction/admin");
    public Task<IReadOnlyList<PaymentListItem>> GetPaymentsAsync() => GetListAsync<PaymentListItem>("api/Payments/admin");
    public Task<IReadOnlyList<ReviewListItem>> GetReviewsAsync() => GetListAsync<ReviewListItem>("api/Reviewes");
    public Task<IReadOnlyList<AddressListItem>> GetAddressesAsync() => GetListAsync<AddressListItem>("api/Addresses");
    public Task<IReadOnlyList<MessageListItem>> GetMessagesAsync() => GetListAsync<MessageListItem>("api/Messages");
    public Task<IReadOnlyList<PhoneListItem>> GetPhonesAsync() => GetListAsync<PhoneListItem>("api/UserPhones/1");
    public Task<IReadOnlyList<ProviderCategoryListItem>> GetProviderCategoriesAsync() => GetListAsync<ProviderCategoryListItem>("api/ProviderCategories");
    public Task<IReadOnlyList<ApplicationHistoryListItem>> GetApplicationHistoriesAsync() => GetListAsync<ApplicationHistoryListItem>("api/ApplicationHistories/1");
    public Task<IReadOnlyList<ProviderStatusHistoryListItem>> GetProviderStatusHistoriesAsync() => GetListAsync<ProviderStatusHistoryListItem>("api/ProviderStatusHistories");
    public Task<IReadOnlyList<OrderStatusHistoryListItem>> GetOrderStatusHistoriesAsync() => GetListAsync<OrderStatusHistoryListItem>("api/OrderStatusHistories/admin");
    public Task<IReadOnlyList<AuditLogListItem>> GetAuditLogsAsync() => Task.FromResult<IReadOnlyList<AuditLogListItem>>(new List<AuditLogListItem>());

    public async Task<CreateOrderRequest?> CreateOrderAsync(CreateOrderRequest request)
    {
        await SendAsync(async () => await _http.PostAsJsonAsync($"api/Orders/{request.UserId}", request, Json));
        return request;
    }

    public async Task<CreateOfferRequest?> CreateOfferAsync(CreateOfferRequest request)
    {
        await SendAsync(async () => await _http.PostAsJsonAsync("api/Offers", request, Json));
        return request;
    }

    public Task AcceptOfferAsync(int offerId) => SendAsync(() => _http.PatchAsync($"api/Offers/accept/{offerId}", null));
    public Task RejectOfferAsync(int offerId) => SendAsync(() => _http.PatchAsync($"api/Offers/reject/{offerId}", null));
    public Task CancelOfferAsync(int offerId, string reason) => SendAsync(() => _http.PatchAsync($"api/Offers/cancel/{offerId}", null));
    public Task CancelOrderAsync(int orderId, string reason, UserType actor)
    {
        var route = actor switch
        {
            UserType.Provider => "provider-cancel",
            UserType.Admin => "admin-cancel",
            _ => "customer-cancel"
        };
        return SendAsync(() => _http.PatchAsJsonAsync($"api/Orders/{route}", new { OrderId = orderId, Reason = reason }, Json));
    }
    public Task StartOrderAsync(int orderId) => SendAsync(() => _http.PatchAsync($"api/Orders/inprogress/{orderId}", null));
    public Task CompleteOrderAsync(int orderId) => SendAsync(() => _http.PatchAsync($"api/Orders/complete/{orderId}", null));
    public Task ApproveDocumentAsync(int documentId) => SendAsync(() => _http.PatchAsJsonAsync("api/ProviderDocuments", new { DocumentId = documentId, Status = DocumentStatus.Approved }, Json));
    public Task RejectDocumentAsync(int documentId, string reason) => SendAsync(() => _http.PatchAsJsonAsync("api/ProviderDocuments", new { DocumentId = documentId, Status = DocumentStatus.ReUploadRequired, Reason = reason }, Json));
    public Task SetProviderAvailabilityAsync(int providerId, bool available) => SendAsync(() => _http.PatchAsJsonAsync($"api/Providers/{providerId}", new { IsAvailable = available }, Json));
}
