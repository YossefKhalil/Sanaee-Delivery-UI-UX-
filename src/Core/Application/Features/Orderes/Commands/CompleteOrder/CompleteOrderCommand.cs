

using MediatR;

namespace Core.Application.Features.Orderes.Commands.CompleteOrder
{
    public sealed record CompleteOrderCommand(int OrderId) : IRequest;
}
