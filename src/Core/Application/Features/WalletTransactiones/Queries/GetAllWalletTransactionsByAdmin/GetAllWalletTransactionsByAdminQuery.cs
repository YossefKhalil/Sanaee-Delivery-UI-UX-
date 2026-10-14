using Core.Application.Common.Models;
using Core.Application.Features.WalletTransactiones.Dtos;
using MediatR;

namespace Core.Application.Features.WalletTransactiones.Queries.GetAllWalletTransactionsByAdmin
{
    public sealed record GetAllWalletTransactionsByAdminQuery
        (
            int PageNumber = 1,
            int PageSize = 10

        ) : IRequest<PagedResult<GetWalletTransactionDto>>
    {
    }
}
