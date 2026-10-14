
using FluentValidation;

namespace Core.Application.Features.Orderes.Commands.CancelOrderByCustomer
{
    public class CancelOrderByCustomerCommandValidator : AbstractValidator<CancelOrderByCustomerCommand>
    {
        public CancelOrderByCustomerCommandValidator()
        {
            RuleFor(o => o.OrderId)
                .NotEmpty().WithMessage("Order Id is required!");

            RuleFor(o => o.ReasonId)
                .NotEmpty().WithMessage("Reason Id is required!");
        }
    }
}
