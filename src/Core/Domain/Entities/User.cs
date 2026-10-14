using Core.Domain.Entities.Contract;
using Core.Domain.Enums;

namespace Core.Domain.Entities
{
    public class User : ISoftDeletable
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserType Type { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public float? AverageRating { get; set; }
        public int? TotalReviewCount { get; set; }
        public bool IsActive { get; set; }


        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<UserPhone> Phones { get; set; } = new List<UserPhone>();
        public Provider? Provider { get; set; }
        public Wallet Wallet { get; set; } = null!;



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
