

using Core.Domain.Enums;
using MediatR;

namespace Core.Application.Features.CancelatoinReasones.Commands.UpdateCancelationReason
{
    public sealed record UpdateCancelationReasonCommand(int ReasonId, string? Text, UserType? ApplicableTo)
        : IRequest;
}
