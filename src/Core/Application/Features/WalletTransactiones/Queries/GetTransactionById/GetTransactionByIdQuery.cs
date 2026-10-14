using Core.Application.Features.WalletTransactiones.Dtos;
using MediatR;

namespace Core.Application.Features.WalletTransactiones.Queries.GetTransactionById
{
    public sealed record GetTransactionByIdQuery(int TransactionId) : IRequest<GetWalletTransactionDto>;
}
