using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using SanaeeUI.Client.Models;
using SanaeeUI.Client.Services.Api;

namespace SanaeeUI.Client.Services;

// Live implementation talking to the real Sanaee-Delivery API.
//
// The API responses are camelCase with string enums. Most list endpoints return a
// PagedResult envelope ({ items: [...] }); a few return a bare array; several are
// per-entity only (e.g. offers/messages require an OrderId), so those are aggregated
// by iterating known parent ids. If the API is unreachable or returns an error the
// call fails gracefully and the UI surfaces LastError.
public class SanaeeApiClient : ISanaeeDataService
{
    private readonly HttpClient _http;

    private static readonly JsonSerializerOptions Json = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    private const int PageSizeAll = 100; // API caps PageSize at 100 per request
    private const int AggregateCap = 20;  // bound N+1 aggregation for per-entity endpoints

    public SanaeeApiClient(HttpClient http) => _http = http;

    public DataMode Mode => DataMode.Live;
    public string? LastError { get; private set; }

    // ---- low-level helpers -------------------------------------------------

    // GET that tolerates a bare array, a PagedResult envelope, or a single object.
    private async Task<List<TApi>> GetRawListAsync<TApi>(string url)
    {
        try
        {
            using var response = await _http.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                LastError = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase} ({url})";
                return new List<TApi>();
            }
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json) || json == "null") return new List<TApi>();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            switch (root.ValueKind)
            {
                case JsonValueKind.Array:
                    return root.Deserialize<List<TApi>>(Json) ?? new List<TApi>();
                case JsonValueKind.Object:
                    if (root.TryGetProperty("items", out var items) && items.ValueKind == JsonValueKind.Array)
                        return items.Deserialize<List<TApi>>(Json) ?? new List<TApi>();
                    var single = root.Deserialize<TApi>(Json);
                    return single is null ? new List<TApi>() : new List<TApi> { single };
                default:
                    return new List<TApi>();
            }
        }
        catch (Exception ex)
        {
            LastError = ex.Message;
            return new List<TApi>();
        }
    }

    private async Task<TApi?> GetRawItemAsync<TApi>(string url) where TApi : class
    {
        try
        {
            using var response = await _http.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                LastError = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase} ({url})";
                return null;
            }
            if (response.StatusCode == HttpStatusCode.NoContent) return null;
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json) || json == "null") return null;
            return JsonSerializer.Deserialize<TApi>(json, Json);
        }
        catch (Exception ex)
        {
            LastError = ex.Message;
            return null;
        }
    }

    // Fetch a list for many parent ids in parallel and flatten the results.
    private async Task<List<TApi>> GetForParentsAsync<TApi>(IEnumerable<int> parentIds, Func<int, string> url)
    {
        var tasks = parentIds.Select(id => GetRawListAsync<TApi>(url(id)));
        var results = await Task.WhenAll(tasks);
        return results.SelectMany(r => r).ToList();
    }

    // Same, but the mapping for each item also receives the parent id (for DTOs
    // that omit it, e.g. messages).
    private async Task<List<TModel>> GetForParentsAsync<TApi, TModel>(
        IEnumerable<int> parentIds, Func<int, string> url, Func<int, TApi, TModel> map)
    {
        var tasks = parentIds.Select(async id =>
        {
            var dtos = await GetRawListAsync<TApi>(url(id));
            return dtos.Select(d => map(id, d)).ToList();
        });
        var results = await Task.WhenAll(tasks);
        return results.SelectMany(r => r).ToList();
    }

    private async Task<bool> SendAsync(HttpMethod method, string url, object? body)
    {
        try
        {
            using var request = new HttpRequestMessage(method, url);
            if (body is not null)
            {
                request.Content = new StringContent(JsonSerializer.Serialize(body, Json), Encoding.UTF8, "application/json");
            }
            using var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                LastError = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase} ({url})";
                return false;
            }
            LastError = null;
            return true;
        }
        catch (Exception ex)
        {
            LastError = ex.Message;
            return false;
        }
    }

    private static string Name(string first, string last) => $"{first} {last}".Trim();
    private static string Address(string title, string region, string city) =>
        string.Join(", ", new[] { title, region, city }.Where(s => !string.IsNullOrWhiteSpace(s)));

    // ---- mappers (API DTO -> UI model) -------------------------------------

    private static OrderListItem MapOrder(GetAllOrdersDto d) => new()
    {
        Id = d.OrderId,
        Status = d.Status,
        Category = d.Name,
        Customer = Name(d.FirstName, d.LastName),
        Address = Address(d.Title, d.Region, d.City),
        AcceptedOfferId = d.AcceptedOfferId,
        CreatedAt = d.CreatedAt
    };

    private static OrderListItem MapOrderDetail(GetOrderDto d) => new()
    {
        Id = d.OrderId,
        Status = d.Status,
        Category = d.Name,
        Customer = Name(d.FirstName, d.LastName),
        Address = Address(d.Title, d.Region, d.City),
        AcceptedOfferId = d.AcceptedOfferId,
        CreatedAt = d.CreatedAt,
        VisitingTime = d.VisitingTime,
        ProblemDescription = d.ProblemDescription ?? string.Empty
    };

    private static OfferListItem MapOffer(GetAllOffersForOrderDto d) => new()
    {
        Id = 0, // the API list DTO does not expose OfferId
        OrderId = d.OrderId,
        Provider = Name(d.FirstName, d.LastName),
        LaborCost = d.LaborCost,
        MaterialCost = d.MaterialCost,
        ArrivalTime = d.ArrivalTime,
        Status = d.Status,
        ExpiresAt = d.ExpiresAt
    };

    private static ProviderListItem MapProvider(ProviderDto d) => new()
    {
        Id = d.ProviderId ?? 0,
        Name = Name(d.FirstName, d.LastName),
        NationalNumber = d.NationalNumber ?? string.Empty,
        IsAvailable = d.IsAvailable ?? false
    };

    private static CustomerListItem MapCustomer(UserDto d) => new()
    {
        Id = d.UserId,
        Name = Name(d.FirstName, d.LastName),
        Email = d.Email,
        AverageRating = d.AverageRating,
        IsActive = !d.IsDeleted,
        CreatedAt = d.CreatedAt
    };

    private static CategoryListItem MapCategory(GetCategoryDto d) => new()
    {
        Id = d.CategoryId,
        Name = d.Name,
        Description = d.Description,
        IconUrl = d.IconUrl,
        IsActive = d.IsActive,
        ParentCategoryId = d.ParentCategoryId
    };

    private static CancelReasonListItem MapCancelReason(CancelationReason d) => new()
    {
        Id = d.ReasonId,
        Text = d.Text,
        ApplicableTo = d.ApplicableTo,
        IsActive = d.IsActive
    };

    private static ProviderDocumentListItem MapDocument(GetDocumentDto d) => new()
    {
        Id = d.DocumentId,
        Provider = Name(d.FirstName, d.LastName),
        Type = d.Type,
        Status = d.Status,
        DocumentUrl = d.DocumentUrl,
        ChangedBy = d.ChangedBy.ToString()
    };

    private static WalletListItem MapWallet(WalletDto d) => new()
    {
        Id = d.WalletId,
        Balance = d.Balance,
        IsActive = d.IsActive
    };

    private static WalletTransactionListItem MapTransaction(GetWalletTransactionDto d) => new()
    {
        Id = d.TransactionId,
        WalletId = d.WalletId,
        Amount = d.Amount,
        Type = d.Type,
        Description = d.Description,
        CreatedAt = d.CreatedAt,
        OrderId = d.OrderId
    };

    private static PaymentListItem MapPayment(GetPaymentDto d) => new()
    {
        Id = d.PaymentId,
        User = Name(d.FirstName, d.LastName),
        Amount = d.Amount,
        Method = d.Method,
        Status = d.Status,
        Type = d.Type,
        TransactionReference = d.TransactionReference,
        CreatedAt = d.CreatedAt,
        PaidAt = d.PaidAt,
        OrderId = d.OrderId
    };

    private static ReviewListItem MapReview(GetReviewsByRevieweeIdDto d) => new()
    {
        Id = d.ReviewId,
        Reviewer = Name(d.FirstName, d.LastName),
        Rating = d.Rating,
        Comment = d.Comment,
        CreatedAt = d.CreatedAt,
        IsEdited = d.IsEdited,
        OrderId = d.OrderId
    };

    private static AddressListItem MapAddress(AddressDto d) => new()
    {
        Id = d.AddressId,
        UserId = d.UserId,
        Title = d.Title,
        City = d.City,
        Region = d.Region,
        Street = d.Street,
        BuildingNumber = d.BuildingNumber,
        ApartmentNumber = d.ApartmentNumber,
        IsDefault = d.IsDefault,
        Latitude = d.Latitude,
        Longitude = d.Longitude
    };

    private static OrderStatusHistoryListItem MapHistory(GetHistoryDto d) => new()
    {
        Id = d.HistoryId,
        OrderId = d.OrderId,
        Status = d.Status,
        Notes = d.Notes,
        CreatedAt = d.CreatedAt,
        By = Name(d.FirstName, d.LastName),
        Reason = d.Text
    };

    // ---- reads -------------------------------------------------------------

    public async Task<IReadOnlyList<OrderListItem>> GetOrdersAsync()
    {
        var dtos = await GetRawListAsync<GetAllOrdersDto>($"api/Orders/admin?pageNumber=1&pageSize={PageSizeAll}");
        return dtos.Select(MapOrder).ToList();
    }

    public async Task<OrderListItem?> GetOrderAsync(int id)
    {
        var dto = await GetRawItemAsync<GetOrderDto>($"api/Orders/{id}");
        return dto is null ? null : MapOrderDetail(dto);
    }

    public async Task<IReadOnlyList<OfferListItem>> GetOffersAsync()
    {
        // Offers are only exposed per order.
        var orderIds = (await GetOrdersAsync()).Select(o => o.Id).Where(id => id != 0).Take(AggregateCap).ToList();
        var dtos = await GetForParentsAsync<GetAllOffersForOrderDto>(orderIds, id => $"api/Offers?OrderId={id}");
        return dtos.Select(MapOffer).ToList();
    }

    public async Task<IReadOnlyList<ProviderListItem>> GetProvidersAsync()
    {
        var baseList = await GetRawListAsync<ProviderDto>($"api/Providers?pageNumber=1&pageSize={PageSizeAll}");
        var ids = baseList.Where(p => p.ProviderId.HasValue).Select(p => p.ProviderId!.Value).Take(AggregateCap).ToList();
        var admins = await GetForParentsAsync<GetProviderForAdminDto>(ids, id => $"api/Providers/admin/{id}");
        var byId = admins.Where(a => a.ProviderId != 0).GroupBy(a => a.ProviderId).ToDictionary(g => g.Key, g => g.First());

        var list = new List<ProviderListItem>();
        foreach (var p in baseList)
        {
            var item = MapProvider(p);
            if (p.ProviderId.HasValue && byId.TryGetValue(p.ProviderId.Value, out var admin))
            {
                item.UserId = admin.UserId;
                item.AverageRating = admin.AverageRating;
                item.TotalReviewCount = admin.TotalReviewCount ?? 0;
                item.CategoriesCount = admin.Categories?.Count ?? 0;
            }
            list.Add(item);
        }
        return list;
    }

    public async Task<IReadOnlyList<CustomerListItem>> GetCustomersAsync()
    {
        var dtos = await GetRawListAsync<UserDto>($"api/Customers/admin?pageNumber=1&pageSize={PageSizeAll}");
        return dtos.Select(MapCustomer).ToList();
    }

    public async Task<IReadOnlyList<CategoryListItem>> GetCategoriesAsync()
    {
        var dtos = await GetRawListAsync<GetCategoryDto>("api/Categories");
        return dtos.Select(MapCategory).ToList();
    }

    public async Task<IReadOnlyList<CancelReasonListItem>> GetCancelReasonsAsync()
    {
        var dtos = await GetRawListAsync<CancelationReason>("api/CancelationReason");
        return dtos.Select(MapCancelReason).ToList();
    }

    public async Task<IReadOnlyList<ProviderDocumentListItem>> GetDocumentsAsync()
    {
        // Documents are only exposed per provider.
        var providerIds = (await GetProvidersAsync()).Select(p => p.Id).Where(id => id != 0).Take(AggregateCap).ToList();
        var dtos = await GetForParentsAsync<GetDocumentDto>(providerIds, id => $"api/ProviderDocuments?id={id}");
        return dtos.Select(MapDocument).ToList();
    }

    public async Task<IReadOnlyList<WalletListItem>> GetWalletsAsync()
    {
        var dtos = await GetRawListAsync<WalletDto>($"api/Wallets/Admin?pageNumber=1&pageSize={PageSizeAll}");
        return dtos.Select(MapWallet).ToList();
    }

    public async Task<IReadOnlyList<WalletTransactionListItem>> GetTransactionsAsync()
    {
        var dtos = await GetRawListAsync<GetWalletTransactionDto>($"api/WalletTransaction/admin?pageNumber=1&pageSize={PageSizeAll}");
        return dtos.Select(MapTransaction).ToList();
    }

    public async Task<IReadOnlyList<PaymentListItem>> GetPaymentsAsync()
    {
        var dtos = await GetRawListAsync<GetPaymentDto>($"api/Payments/admin?pageNumber=1&pageSize={PageSizeAll}");
        return dtos.Select(MapPayment).ToList();
    }

    public async Task<IReadOnlyList<ReviewListItem>> GetReviewsAsync()
    {
        // Reviews are only exposed per reviewee (provider or customer).
        var providerIds = (await GetProvidersAsync()).Select(p => p.Id).Where(id => id != 0).ToList();
        var customerIds = (await GetCustomersAsync()).Select(c => c.Id).Where(id => id != 0).ToList();
        var ids = providerIds.Concat(customerIds).Distinct().Take(AggregateCap).ToList();
        var dtos = await GetForParentsAsync<GetReviewsByRevieweeIdDto>(ids, id => $"api/Reviewes?RevieweeId={id}");
        return dtos.Select(MapReview).ToList();
    }

    public async Task<IReadOnlyList<AddressListItem>> GetAddressesAsync()
    {
        var customerIds = (await GetCustomersAsync()).Select(c => c.Id).Where(id => id != 0).Take(AggregateCap).ToList();
        var dtos = await GetForParentsAsync<AddressDto>(customerIds, id => $"api/Addresses/{id}");
        return dtos.Select(MapAddress).ToList();
    }

    public async Task<IReadOnlyList<MessageListItem>> GetMessagesAsync()
    {
        var orderIds = (await GetOrdersAsync()).Select(o => o.Id).Where(id => id != 0).Take(AggregateCap).ToList();
        return await GetForParentsAsync<GetMessageDto, MessageListItem>(orderIds, id => $"api/Messages?OrderId={id}",
            (orderId, d) => new MessageListItem
            {
                OrderId = orderId,
                Sender = d.SenderId.ToString(),
                Recipient = d.RecipientId.ToString(),
                Content = d.Content,
                Type = d.Type,
                SentAt = d.SentAt,
                ReadAt = d.ReadAt,
                IsSystemMessage = d.IsSystemMessage
            });
    }

    public async Task<IReadOnlyList<PhoneListItem>> GetPhonesAsync()
    {
        var customerIds = (await GetCustomersAsync()).Select(c => c.Id).Where(id => id != 0).Take(AggregateCap).ToList();
        var dtos = await GetForParentsAsync<GetUserPhonesDto>(customerIds, id => $"api/UserPhones/{id}");
        return dtos.Select(d => new PhoneListItem { Id = d.PhoneId, PhoneNumber = d.PhoneNumber ?? string.Empty }).ToList();
    }

    public async Task<IReadOnlyList<ProviderCategoryListItem>> GetProviderCategoriesAsync()
    {
        // Exposed through each provider's admin detail (Categories).
        var providerIds = (await GetProvidersAsync()).Select(p => p.Id).Where(id => id != 0).Take(AggregateCap).ToList();
        var admins = await GetForParentsAsync<GetProviderForAdminDto>(providerIds, id => $"api/Providers/admin/{id}");
        var list = new List<ProviderCategoryListItem>();
        foreach (var a in admins)
        {
            foreach (var c in a.Categories ?? new List<CategoryDtoHelper>())
            {
                list.Add(new ProviderCategoryListItem
                {
                    Id = c.ProviderCategoryId,
                    Provider = Name(a.FirstName, a.LastName),
                    Category = c.Name,
                    YearsOfExperience = c.YearsOfExperience
                });
            }
        }
        return list;
    }

    public async Task<IReadOnlyList<ApplicationHistoryListItem>> GetApplicationHistoriesAsync()
    {
        var providerIds = (await GetProvidersAsync()).Select(p => p.Id).Where(id => id != 0).Take(AggregateCap).ToList();
        var dtos = await GetForParentsAsync<GetApplicationHistoriesDto>(providerIds, id => $"api/ApplicationHistories/{id}");
        return dtos.Select(d => new ApplicationHistoryListItem
        {
            Id = d.HistoryId,
            Provider = Name(d.FirstName, d.LastName),
            Status = d.Status,
            Notes = d.Notes,
            ChangedAt = d.ChangedAt,
            ChangedBy = d.ChangedBy.ToString()
        }).ToList();
    }

    public async Task<IReadOnlyList<ProviderStatusHistoryListItem>> GetProviderStatusHistoriesAsync()
    {
        var providerIds = (await GetProvidersAsync()).Select(p => p.Id).Where(id => id != 0).Take(AggregateCap).ToList();
        var dtos = await GetForParentsAsync<GetProviderStatusHistoriesDto>(providerIds, id => $"api/ProviderStatusHistories?ProviderId={id}");
        return dtos.Select(d => new ProviderStatusHistoryListItem
        {
            Id = d.HistoryId,
            Provider = Name(d.FirstName, d.LastName),
            Status = d.Status,
            Notes = d.Notes,
            ChangedAt = d.ChangedAt,
            ChangedBy = d.ChangedBy.ToString()
        }).ToList();
    }

    public async Task<IReadOnlyList<OrderStatusHistoryListItem>> GetOrderStatusHistoriesAsync()
    {
        var dtos = await GetRawListAsync<GetHistoryDto>($"api/OrderStatusHistories/admin?pageNumber=1&pageSize={PageSizeAll}");
        return dtos.Select(MapHistory).ToList();
    }

    public Task<IReadOnlyList<AuditLogListItem>> GetAuditLogsAsync()
        => Task.FromResult<IReadOnlyList<AuditLogListItem>>(new List<AuditLogListItem>()); // no audit endpoint exists

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

    // ---- mutations ---------------------------------------------------------

    public async Task<CreateOrderRequest?> CreateOrderAsync(CreateOrderRequest request)
    {
        // POST api/Orders/{UserId} — body is CreateOrderDto (UserId lives in the route).
        var body = new
        {
            visitingTime = request.VisitingTime,
            problemPhoto = request.ProblemPhoto,
            problemDescription = request.ProblemDescription,
            categoryId = request.CategoryId,
            addressId = request.AddressId
        };
        var ok = await SendAsync(HttpMethod.Post, $"api/Orders/{request.UserId}", body);
        return ok ? request : null;
    }

    public async Task<CreateOfferRequest?> CreateOfferAsync(CreateOfferRequest request)
    {
        var body = new
        {
            orderId = request.OrderId,
            notes = request.Notes,
            providerId = request.ProviderId,
            laborCost = request.LaborCost,
            arrivalTime = request.ArrivalTime,
            materialCost = request.MaterialCost
        };
        var ok = await SendAsync(HttpMethod.Post, "api/Offers", body);
        return ok ? request : null;
    }

    // The offers list DTO does not expose OfferId, so the UI cannot supply a real id.
    // These are wired correctly against the endpoints but need an OfferId to succeed.
    public Task AcceptOfferAsync(int offerId) => SendAsync(HttpMethod.Patch, $"api/Offers/accept/{offerId}", null);
    public Task RejectOfferAsync(int offerId) => SendAsync(HttpMethod.Patch, $"api/Offers/reject/{offerId}", null);
    public Task CancelOfferAsync(int offerId, string reason) => SendAsync(HttpMethod.Patch, $"api/Offers/cancel/{offerId}", null);

    public Task CancelOrderAsync(int orderId, string reason, UserType actor)
    {
        var route = actor switch
        {
            UserType.Provider => "provider-cancel",
            UserType.Admin => "admin-cancel",
            _ => "customer-cancel"
        };
        // admin-cancel expects CancelOrderByAdminCommand(OrderId, ReasonId, Notes, IssueRefund).
        // We only have a free-text reason, so ReasonId=0 and the text goes in Notes.
        var body = new { orderId, reasonId = 0, notes = reason, issueRefund = false };
        return SendAsync(HttpMethod.Patch, $"api/Orders/{route}", body);
    }

    public Task StartOrderAsync(int orderId) => SendAsync(HttpMethod.Patch, $"api/Orders/inprogress/{orderId}", null);
    public Task CompleteOrderAsync(int orderId) => SendAsync(HttpMethod.Patch, $"api/Orders/complete/{orderId}", null);

    public Task ApproveDocumentAsync(int documentId) =>
        SendAsync(HttpMethod.Patch, "api/ProviderDocuments", new { documentId, status = DocumentStatus.Approved, adminId = 1 });

    public Task RejectDocumentAsync(int documentId, string reason) =>
        SendAsync(HttpMethod.Patch, "api/ProviderDocuments", new { documentId, status = DocumentStatus.ReUploadRequired, adminId = 1 });

    // The API endpoint toggles availability (ChangeAvailability(Id)); it takes no body,
    // so the requested value is ignored and the current state is flipped.
    public Task SetProviderAvailabilityAsync(int providerId, bool available) =>
        SendAsync(HttpMethod.Patch, $"api/Providers/{providerId}", null);
}
