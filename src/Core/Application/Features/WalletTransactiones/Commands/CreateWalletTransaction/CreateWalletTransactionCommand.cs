using Core.Domain.Enums;
using MediatR;

namespace Core.Application.Features.WalletTransactiones.Commands.CreateWalletTransaction
{
    public sealed record CreateWalletTransactionCommand
        (
            decimal Amount,
            TransactionType Type,
            string Description,
            int WalletId,
            int? OrderId

        ) : IRequest<int>;
}
