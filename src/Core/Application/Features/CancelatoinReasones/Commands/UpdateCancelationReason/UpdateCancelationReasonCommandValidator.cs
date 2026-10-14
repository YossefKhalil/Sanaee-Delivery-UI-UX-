

using FluentValidation;

namespace Core.Application.Features.CancelatoinReasones.Commands.UpdateCancelationReason
{
    public class UpdateCancelationReasonCommandValidator : AbstractValidator<UpdateCancelationReasonCommand>
    {
        public UpdateCancelationReasonCommandValidator()
        {
            RuleFor(cr => cr.Text)
                .MaximumLength(250).WithMessage("The Text exeed the limit");

            RuleFor(cr => cr.ApplicableTo)
                .IsInEnum().WithMessage("invaldi User Type!");

            RuleFor(cr => cr.ReasonId)
                .NotEmpty().WithMessage("Reason Id is required!");
        }
    }
}
