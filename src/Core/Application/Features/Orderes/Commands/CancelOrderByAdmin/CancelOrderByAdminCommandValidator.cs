

using FluentValidation;

namespace Core.Application.Features.Orderes.Commands.CancelOrderByAdmin
{
    public class CancelOrderByAdminCommandValidator : AbstractValidator<CancelOrderByAdminCommand>
    {
        public CancelOrderByAdminCommandValidator()
        {
            RuleFor(o => o.OrderId)
                .NotEmpty().WithMessage("Order Id is required!");

            RuleFor(o => o.ReasonId)
                .NotEmpty().WithMessage("Reason Id is required!");

            RuleFor(o => o.IssueRefund)
                .NotNull().WithMessage("Issue Refund is required!");
        }
    }
}
