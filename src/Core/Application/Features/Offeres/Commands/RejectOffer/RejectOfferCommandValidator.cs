

using FluentValidation;

namespace Core.Application.Features.Offeres.Commands.RejectOffer
{
    public class RejectOfferCommandValidator : AbstractValidator<RejectOfferCommand>
    {
        public RejectOfferCommandValidator()
        {
            RuleFor(o => o.OfferId)
                .NotEmpty().WithMessage("Offer Id is required!");
        }
    }
}
