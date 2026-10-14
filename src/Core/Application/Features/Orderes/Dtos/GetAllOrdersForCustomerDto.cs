using Core.Domain.Enums;

namespace Core.Application.Features.Orderes.Dtos
{
    public class GetAllOrdersForCustomerDto
    {
        public int OrderId { get; set; }
        public string? ProblemDescription { get; set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? AcceptedOfferId { get; set; }
        public int? ProviderId { get; set; }
    }
}
