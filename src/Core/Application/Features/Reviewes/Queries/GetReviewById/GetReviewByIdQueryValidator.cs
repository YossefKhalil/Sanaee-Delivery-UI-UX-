using FluentValidation;

namespace Core.Application.Features.Reviewes.Queries.GetReviewById
{
    public class GetReviewByIdQueryValidator : AbstractValidator<GetReviewByIdQuery>
    {
        public GetReviewByIdQueryValidator()
        {
            RuleFor(r => r.ReviewId)
                .NotEmpty().WithMessage("Review Id is required!");
        }
    }
}
