using Core.Domain.Enums;

namespace Core.Application.Features.Offeres.Dtos
{
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
}
