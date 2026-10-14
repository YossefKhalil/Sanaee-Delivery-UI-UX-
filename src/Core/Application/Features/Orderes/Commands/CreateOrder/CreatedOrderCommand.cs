using Core.Application.Features.Orderes.Dtos;
using MediatR;

namespace Core.Application.Features.Orderes.Commands.CreateOrder
{
    public sealed record CreatedOrderCommand
        (
            CreateOrderDto dto,
            int UserId

        ) : IRequest<int>;
}
