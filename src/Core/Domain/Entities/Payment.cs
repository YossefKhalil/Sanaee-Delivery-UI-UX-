using Core.Domain.Enums;
using Core.Domain.Exceptions;

namespace Core.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; private set; }
        public decimal Amount { get; private set; }
        public PaymentMethod Method { get; private set; }
        public PaymentStatus Status { get; private set; }
        public PaymentType Type { get; private set; }
        public string TransactionReference { get; private set; } = string.Empty;
        public DateTime? PaidAt { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;


        public int UserId { get; private set; }
        public User User { get; set; } = null!;


        public int? OrderId { get; private set; }
        public Order? Order { get; set; }


        protected Payment() { }

        public Payment(decimal amount, PaymentMethod method, PaymentType type, int userId, int? orderId = null)
        {
            Amount = amount;
            Method = method;
            Type = type;
            UserId = userId;
            OrderId = orderId;
            Status = PaymentStatus.Pending;
        }

        public void MarkAsSuccess(string transactionReference)
        {
            if (Status == PaymentStatus.Success) return;

            if (Status == PaymentStatus.Refunded)
                throw new DomainException("لا يمكن تغيير حالة عملية دفع تم استردادها مسبقاً.");

            Status = PaymentStatus.Success;
            TransactionReference = transactionReference;
            PaidAt = DateTime.UtcNow;
        }

        public void MarkAsFailed()
        {
            if (Status == PaymentStatus.Success || Status == PaymentStatus.Refunded)
                throw new DomainException("لا يمكن تغيير حالة عملية دفع مكتملة أو مستردة إلى فاشلة.");

            Status = PaymentStatus.Failed;
        }

        public void Refund()
        {
            if (Status != PaymentStatus.Success)
                throw new DomainException("لا يمكن استرداد مبلغ لعملية دفع لم تكتمل بنجاح.");

            Status = PaymentStatus.Refunded;
        }
    }
}
