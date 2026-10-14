

using FluentValidation;

namespace Core.Application.Features.Categories.Commands.ActivateCategory
{
    public class ActivateCategoryCommandValidator : AbstractValidator<ActivateCategoryCommand>
    {
        public ActivateCategoryCommandValidator()
        {
            RuleFor(c => c.CategoryId)
                .NotEmpty().WithMessage("Category Id is required!");
        }
    }
}
