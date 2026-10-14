

using Core.Application.Common.Models;
using Core.Application.Features.Categories.Dtos;
using MediatR;

namespace Core.Application.Features.Categories.Queries.GetAllCategories
{
    public sealed record GetAllCategoriesQuery
        (
            int PageNumber = 1,
            int PageSize = 10,
            string? SearchTerm = null

        ) : IRequest<PagedResult<GetCategoryDto>>;
}
