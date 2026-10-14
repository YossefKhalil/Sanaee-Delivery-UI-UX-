

using MediatR;

namespace Core.Application.Features.Categories.Commands.DeleteCategory
{
    public sealed record DeleteCategoryCommand(int CategoryId) : IRequest;
    
}
