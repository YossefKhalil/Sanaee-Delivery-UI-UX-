

using FluentValidation;

namespace Core.Application.Features.CancelatoinReasones.Commands.DeleteCancelationReason
{
    public class DeleteCancelationReasonCommandValidator : AbstractValidator<DeleteCancelationReasonCommand>
    {
        public DeleteCancelationReasonCommandValidator()
        {
            RuleFor(cr => cr.ReasonId)
                .NotEmpty().WithMessage("Reason Id is required!");
        }
    }
}
