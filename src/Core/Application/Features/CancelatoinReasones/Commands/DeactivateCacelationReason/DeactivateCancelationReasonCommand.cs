using MediatR;

namespace Core.Application.Features.CancelatoinReasones.Commands.DeactivateCacelationReason
{
    public sealed record DeactivateCancelationReasonCommand(int ReasonId) : IRequest;
}
