

using MediatR;

namespace Core.Application.Features.Categories.Commands.CreateCategory
{
    public sealed record CreateCategoryCommand
        (
            string Name,
            string? Description,
            string? IconUrl,
            int? ParentCategoryId

        ) : IRequest<int>;
    
}
