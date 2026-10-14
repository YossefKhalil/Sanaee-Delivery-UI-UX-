

namespace Core.Domain.Entities
{
    public class UserPhone
    {
        public int PhoneId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsDefault { get; set; } = false;


        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
