

namespace Core.Domain.Entities
{
    public class Wallet
    {
        public int WalletId { get; private set; }
        public decimal Balance { get; private set; }
        public bool IsActive { get; private set; } = true;
        public byte[] RowVersion { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }

        // Relationships
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;

        public ICollection<WalletTransaction> WalletTransactions { get; private set; } = new List<WalletTransaction>();

        public Wallet() { }

        // Constructor للإنشاء
        public Wallet(int userId)
        {
            UserId = userId;
            Balance = 0;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        // Domain Logic Methods (تضمن ثبات قاعدة البيانات)
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be greater than 0.");

            if (!IsActive)
                throw new InvalidOperationException("Wallet is freezeing now.");

            Balance += amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be greater than 0.");

            if (!IsActive)
                throw new InvalidOperationException("Wallet is freezeing now.");

            if (Balance < amount)
                throw new InvalidOperationException("Your balance does not enough to complete operation.");

            Balance -= amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Freeze()
        {
            // check if it is already Deactivated
            if (!IsActive) return;

            IsActive = false;
        }
        public void Unfreeze()
        {
            // check if it is already Activated
            if (IsActive) return;

            IsActive = true;
        }

    }
}
