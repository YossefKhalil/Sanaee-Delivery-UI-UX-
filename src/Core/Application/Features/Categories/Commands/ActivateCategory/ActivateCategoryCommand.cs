

using MediatR;

namespace Core.Application.Features.Categories.Commands.ActivateCategory
{
    public sealed record ActivateCategoryCommand(int CategoryId) : IRequest;
}
