
using Core.Domain.Enums;

namespace Core.Application.Features.OrderStatusHistories.Dtos
{
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
}
