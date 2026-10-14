

using Core.Application.Features.Orderes.Commands.CancelOrderByProvider;
using FluentValidation;

namespace Application.Features.Orderes.Commands.CancelOrderByProvider
{
    public class CancelOrderByProviderCommandValidator : AbstractValidator<CancelOrderByProviderCommand>
    {
        public CancelOrderByProviderCommandValidator()
        {
            RuleFor(o => o.OrderId)
                .NotEmpty().WithMessage("Order Id is required!");

            RuleFor(o => o.ReasonId)
                .NotEmpty().WithMessage("Reason Id is required!");
        }
    }
}
