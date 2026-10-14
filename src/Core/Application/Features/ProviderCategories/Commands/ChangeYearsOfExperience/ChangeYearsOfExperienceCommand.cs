

using MediatR;

namespace Core.Application.Features.ProviderCategories.Commands.ChangeYearsOfExperience
{
    public sealed record ChangeYearsOfExperienceCommand(int ProviderCategoryId , int YearsOfExperience) : IRequest;
}
