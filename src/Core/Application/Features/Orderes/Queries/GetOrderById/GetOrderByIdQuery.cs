

using Core.Application.Features.Orderes.Dtos;
using MediatR;

namespace Core.Application.Features.Orderes.Queries.GetOrderById
{
    public sealed record GetOrderByIdQuery(int OrderId) : IRequest<GetOrderDto>;
}
