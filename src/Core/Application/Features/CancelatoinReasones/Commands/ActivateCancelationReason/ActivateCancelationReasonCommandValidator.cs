using FluentValidation;

namespace Core.Application.Features.CancelatoinReasones.Commands.ActivateCancelationReason
{
    public class ActivateCancelationReasonCommandValidator : AbstractValidator<ActivateCancelationReasonCommand>
    {
        public ActivateCancelationReasonCommandValidator()
        {
            RuleFor(cr => cr.ReasonId)
                .NotEmpty().WithMessage("Reason Id is required!");
        }
    }
}
