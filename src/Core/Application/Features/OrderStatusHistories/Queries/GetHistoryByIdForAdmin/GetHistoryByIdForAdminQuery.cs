

using Core.Application.Features.OrderStatusHistories.Dtos;
using MediatR;

namespace Core.Application.Features.OrderStatusHistories.Queries.GetHistoryByIdForAdmin
{
    public sealed record GetHistoryByIdForAdminQuery(int HistoryId) : IRequest<GetHistoryDto>;
}
