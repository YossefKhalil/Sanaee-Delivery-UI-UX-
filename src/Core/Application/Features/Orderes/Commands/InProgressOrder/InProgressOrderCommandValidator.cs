

using FluentValidation;

namespace Core.Application.Features.Orderes.Commands.InProgressOrder
{
    public class InProgressOrderCommandValidator : AbstractValidator<InProgressOrderCommand>
    {
        public InProgressOrderCommandValidator()
        {
            RuleFor(o => o.OrderId)
                .NotEmpty().WithMessage("Order Id is required!");
        }
    }
}
