using FluentValidation;

namespace Application.Features.CancelatoinReasones.Commands.CreateCancelationReason
{
    public class CreateCancelationReasonCommandValidator : AbstractValidator<CreateCancelationReasonCommand>
    {
        public CreateCancelationReasonCommandValidator()
        {
            RuleFor(cr => cr.Text)
                .NotEmpty().WithMessage("Text is required!")
                .MaximumLength(250).WithMessage("The Text exeed the limit");

            RuleFor(cr => cr.ApplicableTo)
                .NotEmpty().WithMessage("User Type is required!")
                .IsInEnum().WithMessage("invaldi User Type!");
        }
    }
}
