

using FluentValidation;

namespace Core.Application.Features.Categories.Commands.RestoreCategory
{
    public class RestoreCategoryCommandValidator : AbstractValidator<RestoreCategoryCommand>
    {
        public RestoreCategoryCommandValidator()
        {
            RuleFor(c => c.CategoryId)
                .NotEmpty().WithMessage("Category Id is required!");
        }
    }
}
