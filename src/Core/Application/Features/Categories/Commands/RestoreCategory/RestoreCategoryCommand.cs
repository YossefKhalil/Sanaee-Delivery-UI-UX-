

using MediatR;

namespace Core.Application.Features.Categories.Commands.RestoreCategory
{
    public sealed record RestoreCategoryCommand(int CategoryId) : IRequest;
}
