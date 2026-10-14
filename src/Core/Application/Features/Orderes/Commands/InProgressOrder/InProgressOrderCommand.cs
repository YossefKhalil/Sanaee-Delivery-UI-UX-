

using MediatR;

namespace Core.Application.Features.Orderes.Commands.InProgressOrder
{
    public sealed record InProgressOrderCommand(int OrderId) : IRequest;
}
