using Core.Domain.Enums;

namespace Core.Application.Features.Pymentes.Dtos
{
    public class GetPaymentDto
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get;  set; }
        public PaymentStatus Status { get; set; }
        public PaymentType Type { get; set; }
        public string TransactionReference { get; set; } = string.Empty;
        public DateTime? PaidAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;


        public int? OrderId { get;  set; }
    }
}
