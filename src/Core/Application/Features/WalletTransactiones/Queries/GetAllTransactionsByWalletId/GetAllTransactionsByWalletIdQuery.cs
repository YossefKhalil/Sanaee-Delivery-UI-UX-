using Core.Application.Features.WalletTransactiones.Dtos;
using MediatR;

namespace Core.Application.Features.WalletTransactiones.Queries.GetAllTransactionsByWalletId
{
    public sealed record GetAllTransactionsByWalletIdQuery(int WalletId) : IRequest<IReadOnlyList<GetWalletTransactionDto>>
    {
    }
}
