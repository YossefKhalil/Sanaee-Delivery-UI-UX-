using System.ComponentModel.DataAnnotations;

namespace SanaeeUI.Client.Models;

// Read models shaped for the dashboard UI. These mirror the query DTOs exposed
// by the Sanaee-Delivery API (Core.Application.Features.*.Dtos) but are flattened
// for grid/list rendering.

public class OrderListItem
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
    public string ProblemDescription { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Customer { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int? AcceptedOfferId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? VisitingTime { get; set; }
    public decimal? AcceptedOfferTotal { get; set; }
    public int OffersCount { get; set; }
}

public class OfferListItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Provider { get; set; } = string.Empty;
    public decimal LaborCost { get; set; }
    public decimal? MaterialCost { get; set; }
    public decimal Total => LaborCost + (MaterialCost ?? 0);
    public DateTime ArrivalTime { get; set; }
    public OfferStatus Status { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string? Notes { get; set; }
}

public class ProviderListItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NationalNumber { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public ProviderStatus Status { get; set; }
    public double? AverageRating { get; set; }
    public int TotalReviewCount { get; set; }
    public int CategoriesCount { get; set; }
    public int CompletedOrders { get; set; }
}

public class CustomerListItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int OrdersCount { get; set; }
    public double? AverageRating { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CategoryListItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
    public bool IsActive { get; set; }
    public int? ParentCategoryId { get; set; }
    public int ProvidersCount { get; set; }
}

public class CancelReasonListItem
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public UserType ApplicableTo { get; set; }
    public bool IsActive { get; set; }
}

public class ProviderDocumentListItem
{
    public int Id { get; set; }
    public string Provider { get; set; } = string.Empty;
    public DocumentType Type { get; set; }
    public DocumentStatus Status { get; set; }
    public string DocumentUrl { get; set; } = string.Empty;
    public string? ChangedBy { get; set; }
    public DateTime? ChangedAt { get; set; }
}

public class WalletListItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string User { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public bool IsActive { get; set; }
}

public class WalletTransactionListItem
{
    public int Id { get; set; }
    public int WalletId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int? OrderId { get; set; }
}

public class PaymentListItem
{
    public int Id { get; set; }
    public string User { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public PaymentStatus Status { get; set; }
    public PaymentType Type { get; set; }
    public string? TransactionReference { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PaidAt { get; set; }
    public int? OrderId { get; set; }
}

public class ReviewListItem
{
    public int Id { get; set; }
    public string Reviewee { get; set; } = string.Empty;
    public string Reviewer { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsEdited { get; set; }
    public int? OrderId { get; set; }
}

public class AddressListItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string? BuildingNumber { get; set; }
    public string? ApartmentNumber { get; set; }
    public bool IsDefault { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}

public class MessageListItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Sender { get; set; } = string.Empty;
    public string Recipient { get; set; } = string.Empty;
    public string? Content { get; set; }
    public MessageType Type { get; set; }
    public DateTime SentAt { get; set; }
    public DateTime? ReadAt { get; set; }
    public bool IsSystemMessage { get; set; }
}

public class PhoneListItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
}

public class ProviderCategoryListItem
{
    public int Id { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
}

public class ApplicationHistoryListItem
{
    public int Id { get; set; }
    public string Provider { get; set; } = string.Empty;
    public ApplicationStatus Status { get; set; }
    public string? Notes { get; set; }
    public DateTime ChangedAt { get; set; }
    public string ChangedBy { get; set; } = string.Empty;
}

public class ProviderStatusHistoryListItem
{
    public int Id { get; set; }
    public string Provider { get; set; } = string.Empty;
    public ProviderStatus Status { get; set; }
    public string? Notes { get; set; }
    public DateTime ChangedAt { get; set; }
    public string ChangedBy { get; set; } = string.Empty;
}

public class OrderStatusHistoryListItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public string By { get; set; } = string.Empty;
    public string? Reason { get; set; }
}

public class AuditLogListItem
{
    public int Id { get; set; }
    public EntityName EntityName { get; set; }
    public int EntityId { get; set; }
    public ActionType Action { get; set; }
    public string? Reason { get; set; }
    public DateTime CreatedDate { get; set; }
    public string PerformedBy { get; set; } = string.Empty;
    public bool IsSelfAction { get; set; }
    public string? IpAddress { get; set; }
}

public class DashboardStats
{
    public int TotalOrders { get; set; }
    public int OpenOrders { get; set; }
    public int InProgressOrders { get; set; }
    public int CompletedOrders { get; set; }
    public int CancelledOrders { get; set; }
    public int TotalProviders { get; set; }
    public int OnlineProviders { get; set; }
    public int PendingDocuments { get; set; }
    public int TotalCustomers { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal WalletLiability { get; set; }
    public double AverageRating { get; set; }
    public List<OrderStatusBucket> OrdersByStatus { get; set; } = new();
    public List<RevenuePoint> RevenueTrend { get; set; } = new();
    public List<CategoryBucket> OrdersByCategory { get; set; } = new();
    public List<CancelReasonBucket> CancellationReasons { get; set; } = new();
}

public class OrderStatusBucket
{
    public OrderStatus Status { get; set; }
    public int Count { get; set; }
}

public class RevenuePoint
{
    public string Label { get; set; } = string.Empty;
    public decimal Value { get; set; }
}

public class CategoryBucket
{
    public string Category { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class CancelReasonBucket
{
    public string Reason { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class CreateOrderRequest
{
    [Required] public int UserId { get; set; }
    [Required] public int CategoryId { get; set; }
    [Required] public int AddressId { get; set; }
    [Required, StringLength(1000, MinimumLength = 10)] public string ProblemDescription { get; set; } = string.Empty;
    public string? ProblemPhoto { get; set; }
    public DateTime? VisitingTime { get; set; }
}

public class CreateOfferRequest
{
    [Required] public int ProviderId { get; set; }
    [Required] public int OrderId { get; set; }
    [Range(1, 1_000_000)] public decimal LaborCost { get; set; }
    public decimal? MaterialCost { get; set; }
    public string? Notes { get; set; }
    public DateTime ArrivalTime { get; set; } = DateTime.Now.AddDays(1);
}
