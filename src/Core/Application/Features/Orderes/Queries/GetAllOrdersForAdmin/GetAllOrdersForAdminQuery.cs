

using Core.Application.Features.Orderes.Dtos;
using Core.Application.Common.Models;
using MediatR;

namespace Core.Application.Features.Orderes.Queries.GetAllOrdersForAdmin
{
    public sealed record GetAllOrdersForAdminQuery(

            int PageNumber = 1,
            int PageSize = 10,
            string? SearchTerm = null

        ) : IRequest<PagedResult<GetAllOrdersDto>>;
    
}
