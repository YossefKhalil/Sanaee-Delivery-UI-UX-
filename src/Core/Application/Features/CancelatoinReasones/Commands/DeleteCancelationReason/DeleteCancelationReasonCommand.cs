

using MediatR;

namespace Core.Application.Features.CancelatoinReasones.Commands.DeleteCancelationReason
{
    public sealed record DeleteCancelationReasonCommand(int ReasonId) : IRequest;
}
