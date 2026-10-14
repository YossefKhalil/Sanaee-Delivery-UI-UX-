
using Core.Application.Features.Categories.Dtos;
using MediatR;

namespace Core.Application.Features.Categories.Commands.UpdateCategory
{
    public sealed record UpdateCategoryCommand
        (
            int CategoryId,
            CategoryDto dto

        ) : IRequest;
    
}
