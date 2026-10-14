

using FluentValidation;

namespace Core.Application.Features.ProviderCategories.Commands.CreateProviderCategory
{
    public class CreateProviderCategoryCommandValidator : AbstractValidator<CreateProviderCategoryCommand>
    {
        public CreateProviderCategoryCommandValidator()
        {
            RuleFor(pc => pc.YearsOfExperience)
                .NotEmpty().WithMessage("Years Of Experience is required!");

            RuleFor(pc => pc.ProviderId)
                .NotEmpty().WithMessage("Provider Id is required!");

            RuleFor(pc => pc.CategoryId)
                .NotEmpty().WithMessage("Category Id is required!");
        }
    }
}
