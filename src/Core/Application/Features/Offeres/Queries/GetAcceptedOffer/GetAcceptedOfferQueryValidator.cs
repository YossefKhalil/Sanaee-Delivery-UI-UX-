

using Core.Application.Features.Offeres.Queries.GetAcceptedOffer;
using FluentValidation;

namespace Application.Features.Offeres.Queries.GetAcceptedOffer
{
    public class GetAcceptedOfferQueryValidator : AbstractValidator<GetAcceptedOfferQuery>
    {
        public GetAcceptedOfferQueryValidator()
        {
            RuleFor(o => o.OrderId)
                .NotEmpty().WithMessage("Order Id is required!");
        }
    }
}
