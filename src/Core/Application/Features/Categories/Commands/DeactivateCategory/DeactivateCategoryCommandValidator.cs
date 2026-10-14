
using FluentValidation;

namespace Core.Application.Features.Categories.Commands.DeactivateCategory
{
    public class DeactivateCategoryCommandValidator : AbstractValidator<DeactivateCategoryCommand>
    {
        public DeactivateCategoryCommandValidator()
        {
            RuleFor(c => c.CategoryId)
                .NotEmpty().WithMessage("Category Id is required!");
        }
    }
}
