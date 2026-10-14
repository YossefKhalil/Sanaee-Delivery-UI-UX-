using FluentValidation;

namespace Core.Application.Features.Reviewes.Commands.UpdateReview
{
    public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidator()
        {
            RuleFor(r => r.ReviewId)
                .NotEmpty().WithMessage("Review Id is required!");

            RuleFor(r => r.Rating)
                .GreaterThanOrEqualTo(0).WithMessage("Rating must be between 0 and 5")
                .LessThanOrEqualTo(5).WithMessage("Rating must be between 0 and 5");

            RuleFor(r => r.Comment)
                .MaximumLength(1000).WithMessage("Comment exceeded the limit!");
        }
    }
}
