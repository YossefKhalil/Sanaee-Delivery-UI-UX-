

using FluentValidation;

namespace Core.Application.Features.Offeres.Commands.CreateOffer
{
    public class CreateOfferCommandValidator : AbstractValidator<CreateOfferCommand>
    {
        public CreateOfferCommandValidator()
        {
            RuleFor(o => o.orderId)
                .NotEmpty().WithMessage("Order Id is required!");

            RuleFor(o => o.providerId)
                .NotEmpty().WithMessage("Provider Id is required!");

            RuleFor(o => o.laborCost)
                .NotEmpty().WithMessage("Loabor Cost Id is required!")
                .GreaterThan(0).WithMessage("Labor Cost must be greater than zero!");

            RuleFor(o => o.arrivalTime)
                .NotEmpty().WithMessage("Arrival Time is required!")
                .GreaterThan(DateTime.UtcNow).WithMessage("Invalid Arrival Time!");
        }
    }
}
