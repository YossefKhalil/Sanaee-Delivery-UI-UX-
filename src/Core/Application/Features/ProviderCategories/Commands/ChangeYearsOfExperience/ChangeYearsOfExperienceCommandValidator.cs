

using FluentValidation;

namespace Core.Application.Features.ProviderCategories.Commands.ChangeYearsOfExperience
{
    public class ChangeYearsOfExperienceCommandValidator : AbstractValidator<ChangeYearsOfExperienceCommand>
    {
        public ChangeYearsOfExperienceCommandValidator()
        {
            RuleFor(pc => pc.ProviderCategoryId)
                .NotEmpty().WithMessage("Provider Category Id is required!");


            RuleFor(pc => pc.YearsOfExperience)
                .NotEmpty().WithMessage("Years Of Experience is required!");
        }
    }
}
