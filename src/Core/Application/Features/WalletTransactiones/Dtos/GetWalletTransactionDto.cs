using Core.Domain.Enums;

namespace Core.Application.Features.WalletTransactiones.Dtos
{
    public class GetWalletTransactionDto
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public int WalletId { get; set; }

        public int? OrderId { get; set; }
    }
}
