

using MediatR;

namespace Core.Application.Features.Categories.Commands.DeactivateCategory
{
    public sealed record DeactivateCategoryCommand(int CategoryId) : IRequest;
}
