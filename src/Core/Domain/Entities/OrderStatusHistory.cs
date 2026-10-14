using Core.Domain.Enums;

namespace Core.Domain.Entities
{
    public class OrderStatusHistory
    {
        public int HistoryId { get; set; }
        public OrderStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }


        public int UserId { get; set; }
        public User User { get; set; } = null!;


        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;


        public int? ReasonId { get; set; }
        public CancelationReason? Reason { get; set; }
    }
}
