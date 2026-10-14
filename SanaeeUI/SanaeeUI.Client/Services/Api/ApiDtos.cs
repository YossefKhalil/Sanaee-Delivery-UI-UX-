using SanaeeUI.Client.Models;

namespace SanaeeUI.Client.Services.Api;

// DTOs mirroring the real Sanaee-Delivery API response shapes (Core.Application.*.Dtos).
// JSON is camelCase with string enums; deserialization is case-insensitive, so the
// C# property casing below matches automatically. These are the *wire* shapes — the
// client maps them into the flattened UI models in Models\Dtos.cs.

// ---- Paged envelope -------------------------------------------------------
// Most list endpoints return this wrapper: { items, pageNumber, pageSize, ... }.
// The client parses "items" off the raw JSON, so this class is only a typed fallback.
public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}

// ---- Orders ---------------------------------------------------------------
public class GetAllOrdersDto
{
    public int OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsClientReviewed { get; set; }
    public bool IsProviderReviewed { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;   // category name
    public int AddressId { get; set; }
    public string Title { get; set; } = string.Empty;  // address title
    public string City { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public int? AcceptedOfferId { get; set; }
    public int? ProviderId { get; set; }
}

public class GetOrderDto : GetAllOrdersDto
{
    public DateTime? VisitingTime { get; set; }
    public string? ProblemPhoto { get; set; }
    public string? ProblemDescription { get; set; }
    public string Street { get; set; } = string.Empty;
}

// ---- Offers (list DTO has NO OfferId — real API gap) ----------------------
public class GetAllOffersForOrderDto
{
    public DateTime ArrivalTime { get; set; }
    public decimal LaborCost { get; set; }
    public decimal? MaterialCost { get; set; }
    public OfferStatus Status { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ProviderId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int OrderId { get; set; }
}

public class GetOfferDto : GetAllOffersForOrderDto
{
    public string? Notes { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Email { get; set; } = string.Empty;
    public float? AverageRating { get; set; }
    public int? TotalReviewCount { get; set; }
}

// ---- Providers ------------------------------------------------------------
public class ProviderDto
{
    public int? ProviderId { get; set; }
    public string? NationalNumber { get; set; }
    public bool? IsAvailable { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class GetProviderForAdminDto
{
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public float? AverageRating { get; set; }
    public int? TotalReviewCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsUserActive { get; set; }
    public int ProviderId { get; set; }
    public string NationalNumber { get; set; } = string.Empty;
    public bool IsProviderAvailable { get; set; }
    public List<CategoryDtoHelper> Categories { get; set; } = new();
}

public class CategoryDtoHelper
{
    public int ProviderCategoryId { get; set; }
    public int YearsOfExperience { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
}

// ---- Customers ------------------------------------------------------------
public class UserDto
{
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public float? AverageRating { get; set; }
    public int? TotalReviewCount { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}

// ---- Categories -----------------------------------------------------------
public class GetCategoryDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
    public bool IsActive { get; set; }
    public int? ParentCategoryId { get; set; }
    public string? ParentCategoryName { get; set; }
}

// ---- Cancellation reasons (domain entity) ---------------------------------
public class CancelationReason
{
    public int ReasonId { get; set; }
    public string Text { get; set; } = string.Empty;
    public UserType ApplicableTo { get; set; }
    public bool IsActive { get; set; }
}

// ---- Provider documents ---------------------------------------------------
public class GetDocumentDto
{
    public int DocumentId { get; set; }
    public DocumentType Type { get; set; }
    public DocumentStatus Status { get; set; }
    public string DocumentUrl { get; set; } = string.Empty;
    public int ProviderId { get; set; }
    public int ChangedBy { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

// ---- Wallets / transactions / payments ------------------------------------
public class WalletDto
{
    public int WalletId { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}

public class GetWalletTransactionDto
{
    public int TransactionId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int WalletId { get; set; }
    public int? OrderId { get; set; }
}

public class GetPaymentDto
{
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public PaymentStatus Status { get; set; }
    public PaymentType Type { get; set; }
    public string TransactionReference { get; set; } = string.Empty;
    public DateTime? PaidAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int? OrderId { get; set; }
}

// ---- Reviews --------------------------------------------------------------
public class GetReviewsByRevieweeIdDto
{
    public int ReviewId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsEdited { get; set; }
    public int ReviewerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int OrderId { get; set; }
}

// ---- Addresses ------------------------------------------------------------
public class AddressDto
{
    public int AddressId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string? BuildingNumber { get; set; }
    public string? ApartmentNumber { get; set; }
    public string? Notes { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public bool IsDefault { get; set; }
    public int UserId { get; set; }
}

// ---- Messages -------------------------------------------------------------
public class GetMessageDto
{
    public string? Content { get; set; }
    public MessageType Type { get; set; }
    public string? AttachmentUrl { get; set; }
    public DateTime SentAt { get; set; }
    public DateTime? ReadAt { get; set; }
    public int? DurationInSeconds { get; set; }
    public bool IsSystemMessage { get; set; }
    public int SenderId { get; set; }
    public int RecipientId { get; set; }
}

// ---- User phones ----------------------------------------------------------
public class GetUserPhonesDto
{
    public int PhoneId { get; set; }
    public string? PhoneNumber { get; set; }
}

// ---- Provider categories --------------------------------------------------
public class GetProviderCategoryDto
{
    public int ProviderCategoryId { get; set; }
    public int YearsOfExperience { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public float? AverageRating { get; set; }
    public int? TotalReviewCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsUserActive { get; set; }
    public int ProviderId { get; set; }
    public string NationalNumber { get; set; } = string.Empty;
    public bool IsProviderAvailable { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
}

// ---- Application / provider-status histories ------------------------------
public class GetApplicationHistoriesDto
{
    public int HistoryId { get; set; }
    public ApplicationStatus Status { get; set; }
    public string? Notes { get; set; }
    public DateTime ChangedAt { get; set; }
    public int ChangedBy { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int ProviderId { get; set; }
}

public class GetProviderStatusHistoriesDto
{
    public int HistoryId { get; set; }
    public ProviderStatus Status { get; set; }
    public string? Notes { get; set; }
    public DateTime ChangedAt { get; set; }
    public int ChangedBy { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int ProviderId { get; set; }
}

// ---- Order status histories ----------------------------------------------
public class GetHistoryDto
{
    public int HistoryId { get; set; }
    public OrderStatus Status { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int OrderId { get; set; }
    public int? ReasonId { get; set; }
    public string Text { get; set; } = string.Empty;
}
