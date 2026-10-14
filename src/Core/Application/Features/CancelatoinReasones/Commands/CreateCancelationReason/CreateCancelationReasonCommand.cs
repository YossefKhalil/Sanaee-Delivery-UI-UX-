

using Core.Domain.Enums;
using MediatR;

namespace Application.Features.CancelatoinReasones.Commands.CreateCancelationReason
{
    public sealed record CreateCancelationReasonCommand
        (
            string Text,
            UserType ApplicableTo

        ) : IRequest<int>;
}
