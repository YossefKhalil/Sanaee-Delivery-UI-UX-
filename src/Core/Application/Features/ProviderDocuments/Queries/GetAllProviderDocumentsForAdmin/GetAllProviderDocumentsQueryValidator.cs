

using FluentValidation;

namespace Core.Application.Features.ProviderDocuments.Queries.GetAllProviderDocuments
{
    public class GetAllProviderDocumentsQueryValidator : AbstractValidator<GetAllProviderDocumentsQuery>
    {
        public GetAllProviderDocumentsQueryValidator()
        {
            RuleFor(d => d.ProviderId)
                .NotEmpty().WithMessage("Provider Id is required");
        }
    }
}
