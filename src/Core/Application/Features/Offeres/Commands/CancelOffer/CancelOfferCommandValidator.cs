
using FluentValidation;

namespace Core.Application.Features.Offeres.Commands.CancelOffer
{
    public class CancelOfferCommandValidator : AbstractValidator<CancelOfferCommand>
    {
        public CancelOfferCommandValidator()
        {
            RuleFor(o => o.OfferId)
                .NotEmpty().WithMessage("Offer Id is required!");
        }
    }
}
