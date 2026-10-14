

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Extentions;
using Core.Application.Common.Models;
using Core.Application.Features.Categories.Dtos;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Categories.Queries.GetAllCategories
{
    public sealed class GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllCategoriesQuery, PagedResult<GetCategoryDto>>
    {
        public async Task<PagedResult<GetCategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cts)
        {
            var query = await unitOfWork.Repository<Category>()
                                  .GetQueryable()
                                  .AsNoTracking()
                                  .OrderByDescending(c => c.CategoryId)
                                  .ProjectTo<GetCategoryDto>(mapper.ConfigurationProvider)
                                  .ToPagedListAsync(request.PageNumber, request.PageSize, cts);

            return query;
        }
    }
}
