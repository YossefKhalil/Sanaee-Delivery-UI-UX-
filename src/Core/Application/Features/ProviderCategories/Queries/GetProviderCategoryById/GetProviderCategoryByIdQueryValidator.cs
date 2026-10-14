
using Core.Application.Features.ProviderCategories.Dtos;
using FluentValidation;

namespace Core.Application.Features.ProviderCategories.Queries.GetProviderCategoryById
{
    public class GetProviderCategoryByIdQueryValidator : AbstractValidator<GetProviderCategoryDto>
    {
        public GetProviderCategoryByIdQueryValidator()
        {
            RuleFor(pc => pc.ProviderCategoryId)
                .NotEmpty().WithMessage("Provider Category Id is required!");
        }
    }
}
