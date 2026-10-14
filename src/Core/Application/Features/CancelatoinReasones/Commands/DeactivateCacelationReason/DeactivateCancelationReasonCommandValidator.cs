using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Application.Features.CancelatoinReasones.Commands.DeactivateCacelationReason
{
    public class DeactivateCancelationReasonCommandValidator : AbstractValidator<DeactivateCancelationReasonCommand>
    {
        public DeactivateCancelationReasonCommandValidator()
        {
            RuleFor(cr => cr.ReasonId)
                .NotEmpty().WithMessage("Reason Id is required!");
        }
    }
}
