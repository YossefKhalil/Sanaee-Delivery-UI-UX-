using FluentValidation;

namespace Core.Application.Features.Provideres.Queries.GetProviderByIdForAdmin
{
    public class GetProviderByIdForAdminQureyValidator : AbstractValidator<GetProviderByIdForAdminQurey>
    {
        public GetProviderByIdForAdminQureyValidator()
        {
            RuleFor(pc => pc.ProviderId)
                .NotEmpty().WithMessage("Provider Id is required!");
        }
    }
}
