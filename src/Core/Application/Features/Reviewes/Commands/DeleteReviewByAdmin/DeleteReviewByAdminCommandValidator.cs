using FluentValidation;

namespace Core.Application.Features.Reviewes.Commands.DeleteReviewByAdmin
{
    public class DeleteReviewByAdminCommandValidator : AbstractValidator<DeleteReviewByAdminCommand>
    {
        public DeleteReviewByAdminCommandValidator()
        {
            RuleFor(r => r.ReviewId)
                .NotEmpty().WithMessage("Review Id is required!");
        }
    }
}
