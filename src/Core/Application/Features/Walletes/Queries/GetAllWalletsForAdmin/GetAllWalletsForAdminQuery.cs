

using Core.Application.Features.Walletes.Dtos;
using Core.Application.Common.Models;
using MediatR;

namespace Core.Application.Features.Walletes.Queries.GetAllWalletsForAdmin
{
    public sealed record GetAllWalletsForAdminQuery(

            int PageNumber = 1,
            int PageSize = 10,
            string? SearchTerm = null

        ) : IRequest<PagedResult<WalletDto>>;
    
}
