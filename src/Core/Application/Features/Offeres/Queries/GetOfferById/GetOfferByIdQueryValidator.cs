

using FluentValidation;

namespace Core.Application.Features.Offeres.Queries.GetOfferById
{
    public class GetOfferByIdQueryValidator : AbstractValidator<GetOfferByIdQuery>
    {
        public GetOfferByIdQueryValidator()
        {
            RuleFor(o => o.OfferId)
                .NotEmpty().WithMessage("Offer Id is required!");
        }
    }
}
