

using MediatR;

namespace Core.Application.Features.ProviderCategories.Commands.CreateProviderCategory
{
    public sealed record CreateProviderCategoryCommand
        (
            int YearsOfExperience,
            int ProviderId,
            int CategoryId

        ) : IRequest<int>;
}
