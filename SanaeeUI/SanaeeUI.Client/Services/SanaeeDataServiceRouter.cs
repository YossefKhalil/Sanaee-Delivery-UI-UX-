using SanaeeUI.Client.Models;

namespace SanaeeUI.Client.Services;

// Routes every call to the active backend (Demo dataset or the live API) based
// on AppState.Mode, without the UI needing to know which one is active.
public class SanaeeDataServiceRouter : ISanaeeDataService
{
    private readonly DemoDataService _demo;
    private readonly SanaeeApiClient _api;
    private readonly AppState _state;

    public SanaeeDataServiceRouter(DemoDataService demo, SanaeeApiClient api, AppState state)
    {
        _demo = demo;
        _api = api;
        _state = state;
    }

    private ISanaeeDataService Active => _state.Mode == DataMode.Live ? _api : _demo;

    public DataMode Mode => Active.Mode;
    public string? LastError => Active.LastError;

    public Task<DashboardStats> GetDashboardStatsAsync() => Active.GetDashboardStatsAsync();
    public Task<IReadOnlyList<OrderListItem>> GetOrdersAsync() => Active.GetOrdersAsync();
    public Task<OrderListItem?> GetOrderAsync(int id) => Active.GetOrderAsync(id);
    public Task<IReadOnlyList<OfferListItem>> GetOffersAsync() => Active.GetOffersAsync();
    public Task<IReadOnlyList<ProviderListItem>> GetProvidersAsync() => Active.GetProvidersAsync();
    public Task<IReadOnlyList<CustomerListItem>> GetCustomersAsync() => Active.GetCustomersAsync();
    public Task<IReadOnlyList<CategoryListItem>> GetCategoriesAsync() => Active.GetCategoriesAsync();
    public Task<IReadOnlyList<CancelReasonListItem>> GetCancelReasonsAsync() => Active.GetCancelReasonsAsync();
    public Task<IReadOnlyList<ProviderDocumentListItem>> GetDocumentsAsync() => Active.GetDocumentsAsync();
    public Task<IReadOnlyList<WalletListItem>> GetWalletsAsync() => Active.GetWalletsAsync();
    public Task<IReadOnlyList<WalletTransactionListItem>> GetTransactionsAsync() => Active.GetTransactionsAsync();
    public Task<IReadOnlyList<PaymentListItem>> GetPaymentsAsync() => Active.GetPaymentsAsync();
    public Task<IReadOnlyList<ReviewListItem>> GetReviewsAsync() => Active.GetReviewsAsync();
    public Task<IReadOnlyList<AddressListItem>> GetAddressesAsync() => Active.GetAddressesAsync();
    public Task<IReadOnlyList<MessageListItem>> GetMessagesAsync() => Active.GetMessagesAsync();
    public Task<IReadOnlyList<PhoneListItem>> GetPhonesAsync() => Active.GetPhonesAsync();
    public Task<IReadOnlyList<ProviderCategoryListItem>> GetProviderCategoriesAsync() => Active.GetProviderCategoriesAsync();
    public Task<IReadOnlyList<ApplicationHistoryListItem>> GetApplicationHistoriesAsync() => Active.GetApplicationHistoriesAsync();
    public Task<IReadOnlyList<ProviderStatusHistoryListItem>> GetProviderStatusHistoriesAsync() => Active.GetProviderStatusHistoriesAsync();
    public Task<IReadOnlyList<OrderStatusHistoryListItem>> GetOrderStatusHistoriesAsync() => Active.GetOrderStatusHistoriesAsync();
    public Task<IReadOnlyList<AuditLogListItem>> GetAuditLogsAsync() => Active.GetAuditLogsAsync();

    public Task<CreateOrderRequest?> CreateOrderAsync(CreateOrderRequest request) => Active.CreateOrderAsync(request);
    public Task<CreateOfferRequest?> CreateOfferAsync(CreateOfferRequest request) => Active.CreateOfferAsync(request);

    public Task AcceptOfferAsync(int offerId) => Active.AcceptOfferAsync(offerId);
    public Task RejectOfferAsync(int offerId) => Active.RejectOfferAsync(offerId);
    public Task CancelOfferAsync(int offerId, string reason) => Active.CancelOfferAsync(offerId, reason);
    public Task CancelOrderAsync(int orderId, string reason, UserType actor) => Active.CancelOrderAsync(orderId, reason, actor);
    public Task StartOrderAsync(int orderId) => Active.StartOrderAsync(orderId);
    public Task CompleteOrderAsync(int orderId) => Active.CompleteOrderAsync(orderId);
    public Task ApproveDocumentAsync(int documentId) => Active.ApproveDocumentAsync(documentId);
    public Task RejectDocumentAsync(int documentId, string reason) => Active.RejectDocumentAsync(documentId, reason);
    public Task SetProviderAvailabilityAsync(int providerId, bool available) => Active.SetProviderAvailabilityAsync(providerId, available);
}
