using SanaeeUI.Client.Models;

namespace SanaeeUI.Client.Services;

public enum DataMode
{
    Demo,
    Live
}

// Single façade the UI depends on. Two implementations exist:
//  - DemoDataService : in-memory dataset (works with no backend running)
//  - SanaeeApiClient : real HTTP calls against the Sanaee-Delivery API
public interface ISanaeeDataService
{
    DataMode Mode { get; }
    string? LastError { get; }

    Task<DashboardStats> GetDashboardStatsAsync();

    Task<IReadOnlyList<OrderListItem>> GetOrdersAsync();
    Task<OrderListItem?> GetOrderAsync(int id);
    Task<IReadOnlyList<OfferListItem>> GetOffersAsync();
    Task<IReadOnlyList<ProviderListItem>> GetProvidersAsync();
    Task<IReadOnlyList<CustomerListItem>> GetCustomersAsync();
    Task<IReadOnlyList<CategoryListItem>> GetCategoriesAsync();
    Task<IReadOnlyList<CancelReasonListItem>> GetCancelReasonsAsync();
    Task<IReadOnlyList<ProviderDocumentListItem>> GetDocumentsAsync();
    Task<IReadOnlyList<WalletListItem>> GetWalletsAsync();
    Task<IReadOnlyList<WalletTransactionListItem>> GetTransactionsAsync();
    Task<IReadOnlyList<PaymentListItem>> GetPaymentsAsync();
    Task<IReadOnlyList<ReviewListItem>> GetReviewsAsync();
    Task<IReadOnlyList<AddressListItem>> GetAddressesAsync();
    Task<IReadOnlyList<MessageListItem>> GetMessagesAsync();
    Task<IReadOnlyList<PhoneListItem>> GetPhonesAsync();
    Task<IReadOnlyList<ProviderCategoryListItem>> GetProviderCategoriesAsync();
    Task<IReadOnlyList<ApplicationHistoryListItem>> GetApplicationHistoriesAsync();
    Task<IReadOnlyList<ProviderStatusHistoryListItem>> GetProviderStatusHistoriesAsync();
    Task<IReadOnlyList<OrderStatusHistoryListItem>> GetOrderStatusHistoriesAsync();
    Task<IReadOnlyList<AuditLogListItem>> GetAuditLogsAsync();

    Task<CreateOrderRequest?> CreateOrderAsync(CreateOrderRequest request);
    Task<CreateOfferRequest?> CreateOfferAsync(CreateOfferRequest request);

    Task AcceptOfferAsync(int offerId);
    Task RejectOfferAsync(int offerId);
    Task CancelOfferAsync(int offerId, string reason);
    Task CancelOrderAsync(int orderId, string reason, UserType actor);
    Task StartOrderAsync(int orderId);
    Task CompleteOrderAsync(int orderId);
    Task ApproveDocumentAsync(int documentId);
    Task RejectDocumentAsync(int documentId, string reason);
    Task SetProviderAvailabilityAsync(int providerId, bool available);
}
