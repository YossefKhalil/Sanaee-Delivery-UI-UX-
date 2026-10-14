

using FluentValidation;

namespace Core.Application.Features.Orderes.Commands.CompleteOrder
{
    public class CompleteOrderCommandValidator : AbstractValidator<CompleteOrderCommand>
    {
        public CompleteOrderCommandValidator()
        {
            RuleFor(o => o.OrderId)
                .NotEmpty().WithMessage("Order Id is required!");
        }
    }
}
