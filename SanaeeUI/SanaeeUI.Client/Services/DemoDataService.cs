using SanaeeUI.Client.Models;

namespace SanaeeUI.Client.Services;

// Fully client-side implementation backed by the in-memory DemoData store.
// This is what makes the UI demonstrable without the API/SQL Server running.
public class DemoDataService : ISanaeeDataService
{
    public DataMode Mode => DataMode.Demo;
    public string? LastError => null;

    public Task<DashboardStats> GetDashboardStatsAsync() => Task.FromResult(DemoData.BuildStats());

    public Task<IReadOnlyList<OrderListItem>> GetOrdersAsync() => List(DemoData.Orders);
    public Task<OrderListItem?> GetOrderAsync(int id) => Task.FromResult(DemoData.Orders.FirstOrDefault(o => o.Id == id));
    public Task<IReadOnlyList<OfferListItem>> GetOffersAsync() => List(DemoData.Offers);
    public Task<IReadOnlyList<ProviderListItem>> GetProvidersAsync() => List(DemoData.Providers);
    public Task<IReadOnlyList<CustomerListItem>> GetCustomersAsync() => List(DemoData.Customers);
    public Task<IReadOnlyList<CategoryListItem>> GetCategoriesAsync() => List(DemoData.CategoriesList);
    public Task<IReadOnlyList<CancelReasonListItem>> GetCancelReasonsAsync() => List(DemoData.CancelReasons);
    public Task<IReadOnlyList<ProviderDocumentListItem>> GetDocumentsAsync() => List(DemoData.Documents);
    public Task<IReadOnlyList<WalletListItem>> GetWalletsAsync() => List(DemoData.Wallets);
    public Task<IReadOnlyList<WalletTransactionListItem>> GetTransactionsAsync() => List(DemoData.WalletTransactions);
    public Task<IReadOnlyList<PaymentListItem>> GetPaymentsAsync() => List(DemoData.Payments);
    public Task<IReadOnlyList<ReviewListItem>> GetReviewsAsync() => List(DemoData.Reviews);
    public Task<IReadOnlyList<AddressListItem>> GetAddressesAsync() => List(DemoData.Addresses);
    public Task<IReadOnlyList<MessageListItem>> GetMessagesAsync() => List(DemoData.Messages);
    public Task<IReadOnlyList<PhoneListItem>> GetPhonesAsync() => List(DemoData.Phones);
    public Task<IReadOnlyList<ProviderCategoryListItem>> GetProviderCategoriesAsync() => List(DemoData.ProviderCategories);
    public Task<IReadOnlyList<ApplicationHistoryListItem>> GetApplicationHistoriesAsync() => List(DemoData.ApplicationHistories);
    public Task<IReadOnlyList<ProviderStatusHistoryListItem>> GetProviderStatusHistoriesAsync() => List(DemoData.ProviderStatusHistories);
    public Task<IReadOnlyList<OrderStatusHistoryListItem>> GetOrderStatusHistoriesAsync() => List(DemoData.OrderStatusHistories);
    public Task<IReadOnlyList<AuditLogListItem>> GetAuditLogsAsync() => List(DemoData.AuditLogs);

    public Task<CreateOrderRequest?> CreateOrderAsync(CreateOrderRequest request)
    {
        var nextId = DemoData.Orders.Count == 0 ? 1 : DemoData.Orders.Max(o => o.Id) + 1;
        var category = DemoData.CategoriesList.FirstOrDefault(c => c.Id == request.CategoryId)?.Name ?? "General";
        DemoData.Orders.Insert(0, new OrderListItem
        {
            Id = nextId,
            Status = OrderStatus.Pending,
            ProblemDescription = request.ProblemDescription,
            Category = category,
            Customer = DemoData.Customers.FirstOrDefault(c => c.Id == request.UserId)?.Name ?? "New customer",
            Address = DemoData.Addresses.FirstOrDefault(a => a.Id == request.AddressId) is { } a ? $"{a.Region}, {a.City}" : "Unknown",
            CreatedAt = DateTime.Now,
            VisitingTime = request.VisitingTime,
            OffersCount = 0
        });
        return Task.FromResult<CreateOrderRequest?>(request);
    }

    public Task<CreateOfferRequest?> CreateOfferAsync(CreateOfferRequest request)
    {
        var nextId = DemoData.Offers.Count == 0 ? 1 : DemoData.Offers.Max(o => o.Id) + 1;
        var order = DemoData.Orders.FirstOrDefault(o => o.Id == request.OrderId);
        DemoData.Offers.Insert(0, new OfferListItem
        {
            Id = nextId,
            OrderId = request.OrderId,
            Provider = DemoData.Providers.FirstOrDefault(p => p.Id == request.ProviderId)?.Name ?? "New provider",
            LaborCost = request.LaborCost,
            MaterialCost = request.MaterialCost,
            ArrivalTime = request.ArrivalTime,
            Status = OfferStatus.Pending,
            ExpiresAt = DateTime.Now.AddDays(2),
            Notes = request.Notes
        });
        if (order is not null)
        {
            order.OffersCount++;
        }
        return Task.FromResult<CreateOfferRequest?>(request);
    }

    public Task AcceptOfferAsync(int offerId)
    {
        var offer = DemoData.Offers.FirstOrDefault(o => o.Id == offerId);
        if (offer is null) return Task.CompletedTask;
        offer.Status = OfferStatus.Accepted;
        var order = DemoData.Orders.FirstOrDefault(o => o.Id == offer.OrderId);
        if (order is not null)
        {
            order.Status = OrderStatus.OfferAccepted;
            order.AcceptedOfferId = offer.Id;
            order.AcceptedOfferTotal = offer.Total;
            foreach (var other in DemoData.Offers.Where(o => o.OrderId == order.Id && o.Id != offer.Id))
            {
                other.Status = OfferStatus.Rejected;
            }
        }
        return Task.CompletedTask;
    }

    public Task RejectOfferAsync(int offerId)
    {
        var offer = DemoData.Offers.FirstOrDefault(o => o.Id == offerId);
        if (offer is not null) offer.Status = OfferStatus.Rejected;
        return Task.CompletedTask;
    }

    public Task CancelOfferAsync(int offerId, string reason)
    {
        var offer = DemoData.Offers.FirstOrDefault(o => o.Id == offerId);
        if (offer is not null) offer.Status = OfferStatus.Cancelled;
        return Task.CompletedTask;
    }

    public Task CancelOrderAsync(int orderId, string reason, UserType actor)
    {
        var order = DemoData.Orders.FirstOrDefault(o => o.Id == orderId);
        if (order is null) return Task.CompletedTask;
        order.Status = OrderStatus.Cancelled;
        order.AcceptedOfferId = null;
        order.AcceptedOfferTotal = null;
        DemoData.OrderStatusHistories.Add(new OrderStatusHistoryListItem
        {
            Id = DemoData.OrderStatusHistories.Count + 1,
            OrderId = order.Id,
            Status = OrderStatus.Cancelled,
            Notes = $"Cancelled by {actor}",
            CreatedAt = DateTime.Now,
            By = actor.ToString(),
            Reason = reason
        });
        return Task.CompletedTask;
    }

    public Task StartOrderAsync(int orderId)
    {
        var order = DemoData.Orders.FirstOrDefault(o => o.Id == orderId);
        if (order is { Status: OrderStatus.OfferAccepted }) order.Status = OrderStatus.InProgress;
        return Task.CompletedTask;
    }

    public Task CompleteOrderAsync(int orderId)
    {
        var order = DemoData.Orders.FirstOrDefault(o => o.Id == orderId);
        if (order is { Status: OrderStatus.InProgress }) order.Status = OrderStatus.Completed;
        return Task.CompletedTask;
    }

    public Task ApproveDocumentAsync(int documentId)
    {
        var doc = DemoData.Documents.FirstOrDefault(d => d.Id == documentId);
        if (doc is null) return Task.CompletedTask;
        doc.Status = DocumentStatus.Approved;
        doc.ChangedBy = "Admin";
        doc.ChangedAt = DateTime.Now;
        return Task.CompletedTask;
    }

    public Task RejectDocumentAsync(int documentId, string reason)
    {
        var doc = DemoData.Documents.FirstOrDefault(d => d.Id == documentId);
        if (doc is null) return Task.CompletedTask;
        doc.Status = DocumentStatus.ReUploadRequired;
        doc.ChangedBy = "Admin";
        doc.ChangedAt = DateTime.Now;
        return Task.CompletedTask;
    }

    public Task SetProviderAvailabilityAsync(int providerId, bool available)
    {
        var provider = DemoData.Providers.FirstOrDefault(p => p.Id == providerId);
        if (provider is not null) provider.IsAvailable = available;
        return Task.CompletedTask;
    }

    private static Task<IReadOnlyList<T>> List<T>(List<T> items) =>
        Task.FromResult<IReadOnlyList<T>>(items.ToList());
}
