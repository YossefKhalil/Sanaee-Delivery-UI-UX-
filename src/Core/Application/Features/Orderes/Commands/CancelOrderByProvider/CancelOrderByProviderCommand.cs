
using MediatR;

namespace Core.Application.Features.Orderes.Commands.CancelOrderByProvider
{
    public sealed record CancelOrderByProviderCommand(int OrderId, int ReasonId, string? Notes) : IRequest;

}
