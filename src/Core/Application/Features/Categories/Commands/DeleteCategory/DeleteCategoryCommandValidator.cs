

using FluentValidation;

namespace Core.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandValidator: AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(c => c.CategoryId)
                .NotEmpty().WithMessage("Category Id is required!");
        }
    }
}
