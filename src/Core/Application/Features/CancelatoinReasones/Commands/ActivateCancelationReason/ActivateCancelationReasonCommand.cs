

using MediatR;

namespace Core.Application.Features.CancelatoinReasones.Commands.ActivateCancelationReason
{
    public sealed record ActivateCancelationReasonCommand(int ReasonId) : IRequest;
}
