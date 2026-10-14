
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Features.Orderes.Dtos;
using Core.Application.Common.Extentions;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Core.Application.Features.Orderes.Queries.GetAllOrdersForAdmin
{
    public sealed class GetAllOrdersForAdminQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllOrdersForAdminQuery, PagedResult<GetAllOrdersDto>>
    {
        public async Task<PagedResult<GetAllOrdersDto>> Handle(GetAllOrdersForAdminQuery request, CancellationToken cts)
        {
            var pagedResult = await unitOfWork.Repository<Order>()
                .GetQueryable()
                .AsNoTracking()
                .OrderByDescending(c => c.Id)
                .ProjectTo<GetAllOrdersDto>(mapper.ConfigurationProvider)
                .ToPagedListAsync(request.PageNumber, request.PageSize, cts);

            return pagedResult;
        }
    }
}
