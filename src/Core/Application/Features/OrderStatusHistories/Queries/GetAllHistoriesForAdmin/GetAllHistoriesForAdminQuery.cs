

using Core.Application.Common.Models;
using Core.Application.Features.OrderStatusHistories.Dtos;
using MediatR;

namespace Core.Application.Features.OrderStatusHistories.Queries.GetAllHistoriesForAdmin
{
    public sealed record GetAllHistoriesForAdminQuery
        (
            int PageNumber = 1,
            int PageSize = 10,
            string? SearchTerm = null

        ) : IRequest<PagedResult<GetHistoryDto>>;
}
