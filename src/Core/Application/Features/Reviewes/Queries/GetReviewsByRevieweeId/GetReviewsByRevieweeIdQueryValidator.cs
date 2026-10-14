using FluentValidation;

namespace Core.Application.Features.Reviewes.Queries.GetReviewsByRevieweeId
{
    public class GetReviewsByRevieweeIdQueryValidator : AbstractValidator<GetReviewsByRevieweeIdQuery>
    {
        public GetReviewsByRevieweeIdQueryValidator()
        {
            RuleFor(r => r.RevieweeId)
                .NotEmpty().WithMessage("Reviewee Id is required!");
        }
    }
}
