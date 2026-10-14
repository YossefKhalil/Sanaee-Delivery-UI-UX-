
using FluentValidation;

namespace Core.Application.Features.ProviderStatusHistories.Queries.GetProviderStatusHistories
{
    public class GetProviderStatusHistoriesQueryValidator : AbstractValidator<GetProviderStatusHistoriesQuery>
    {
        public GetProviderStatusHistoriesQueryValidator()
        {
            RuleFor(psh => psh.ProviderId)
                .NotEmpty().WithMessage("Provider Id is required!");
        }
    }
}
