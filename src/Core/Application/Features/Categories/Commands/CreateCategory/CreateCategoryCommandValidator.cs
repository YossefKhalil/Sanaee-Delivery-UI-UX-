

using FluentValidation;

namespace Core.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Name)
                .MaximumLength(100).WithMessage("The length exceeds the limit!")
                .NotEmpty().WithMessage("Name is required!");

            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("The length exceeds the limit!");
        }
    }
}
