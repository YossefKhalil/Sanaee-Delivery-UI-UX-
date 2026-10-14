

using FluentValidation;

namespace Core.Application.Features.Offeres.Commands.AcceptOffer
{
    public class AcceptOfferCommandValidator : AbstractValidator<AcceptOfferCommand>
    {
        public AcceptOfferCommandValidator()
        {
            RuleFor(o => o.OfferId)
                .NotEmpty().WithMessage("Offer Id is required!");
        }
    }
}
