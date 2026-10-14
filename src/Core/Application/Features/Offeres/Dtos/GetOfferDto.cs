
using Core.Domain.Enums;

namespace Core.Application.Features.Offeres.Dtos
{
    public class GetOfferDto
    {
        public DateTime ArrivalTime { get; set; }
        public decimal LaborCost { get; set; }
        public decimal? MaterialCost { get; set; }
        public string? Notes { get; set; }
        public OfferStatus Status { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int ProviderId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public float? AverageRating { get; set; }
        public int? TotalReviewCount { get; set; }

        public int OrderId { get; set; }
    }
}
