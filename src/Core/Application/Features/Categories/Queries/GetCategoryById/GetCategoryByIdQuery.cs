

using Core.Application.Features.Categories.Dtos;
using MediatR;

namespace Core.Application.Features.Categories.Queries.GetCategoryById
{
    public sealed record GetCategoryByIdQuery(int CategoryId) : IRequest<GetCategoryDto>;
}
