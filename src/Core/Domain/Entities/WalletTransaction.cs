using Core.Domain.Enums;

namespace Core.Domain.Entities
{
    public class WalletTransaction
    {
        public int TransactionId { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }


        public int WalletId { get; private set; }
        public Wallet Wallet { get; private set; } = null!;


        public int? OrderId { get; private set; }
        public Order? Order { get; private set; }


        // Constructor فارغ خاص بـ EF Core
        protected WalletTransaction() { }

        // Constructor لإنشاء المعاملة المالية بشكل آمن
        public WalletTransaction(decimal amount, TransactionType type, string description, int walletId, int? orderId = null)
        {
            if (amount <= 0)
                throw new ArgumentException("قيمة المعاملة المالية يجب أن تكون أكبر من الصفر.", nameof(amount));

            Amount = amount;
            Type = type;
            Description = description;
            WalletId = walletId;
            OrderId = orderId;
        }
    }
}
