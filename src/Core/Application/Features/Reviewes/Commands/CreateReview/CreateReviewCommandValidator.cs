using FluentValidation;

namespace Core.Application.Features.Reviewes.Commands.CreateReview
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(r => r.ReviewerId)
                .NotEmpty().WithMessage("Reviewr Id is required!");

            RuleFor(r => r.dto.Rating)
                .NotEmpty().WithMessage("Rating is required!")
                .GreaterThanOrEqualTo(0).WithMessage("Rating must be between 0 and 5")
                .LessThanOrEqualTo(5).WithMessage("Rating must be between 0 and 5");

            RuleFor(r => r.dto.Comment)
                .NotEmpty().WithMessage("Comment is required!")
                .MaximumLength(1000).WithMessage("Comment exceeded the limit!");

            RuleFor(r => r.dto.RevieweeId)
                .NotEmpty().WithMessage("Reviewee Id is required!");

            RuleFor(r => r.dto.OrderId)
                .NotEmpty().WithMessage("Order Id is required!");
        }
    }
}
