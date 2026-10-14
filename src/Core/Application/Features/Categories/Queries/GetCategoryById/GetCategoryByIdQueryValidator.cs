

using Core.Application.Features.Categories.Dtos;
using FluentValidation;

namespace Core.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(c => c.CategoryId)
                .NotEmpty().WithMessage("Category Id is required!");
        }
    }
}
