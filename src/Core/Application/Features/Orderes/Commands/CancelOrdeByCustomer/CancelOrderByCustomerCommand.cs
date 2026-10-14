

using MediatR;

namespace Core.Application.Features.Orderes.Commands.CancelOrderByCustomer
{
    public sealed record CancelOrderByCustomerCommand(int OrderId, int ReasonId, string? Notes) : IRequest;
}
