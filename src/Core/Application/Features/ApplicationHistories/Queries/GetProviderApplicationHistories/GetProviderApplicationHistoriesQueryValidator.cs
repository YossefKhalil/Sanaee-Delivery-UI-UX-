

using FluentValidation;

namespace Core.Application.Features.ApplicationHistories.Queries.GetProviderApplicationHistories
{
    public class GetProviderApplicationHistoriesQueryValidator : AbstractValidator<GetProviderApplicationHistoriesQuery>
    {
        public GetProviderApplicationHistoriesQueryValidator()
        {
            RuleFor(ah => ah.ProviderID)
                .NotEmpty().WithMessage("Provider ID is required!");
        }
    }
}
