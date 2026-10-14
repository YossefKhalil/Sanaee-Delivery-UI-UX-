

using FluentValidation;

namespace Core.Application.Features.Provideres.Queries.GetProviderById
{
    public class GetProviderByIdQueryValidator : AbstractValidator<GetProviderByIdQuery>
    {
        public GetProviderByIdQueryValidator()
        {
            RuleFor(p => p.ProviderId)
                .NotEmpty().WithMessage("Provider Id is required!");
        }
    }
}
