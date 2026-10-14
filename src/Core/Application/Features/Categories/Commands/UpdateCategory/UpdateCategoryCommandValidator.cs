
using FluentValidation;

namespace Core.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(c => c.dto.Name)
                .MaximumLength(100).WithMessage("The length exceeds the limit!");

            RuleFor(c => c.dto.Description)
                .MaximumLength(500).WithMessage("The length exceeds the limit!");
        }
    }
}
