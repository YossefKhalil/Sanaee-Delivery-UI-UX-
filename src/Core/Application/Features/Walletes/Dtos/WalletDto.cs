

namespace Core.Application.Features.Walletes.Dtos
{
    public class WalletDto
    {
        public int WalletId { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
