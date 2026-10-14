
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Extentions;
using Core.Application.Common.Models;
using Core.Application.Features.OrderStatusHistories.Dtos;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.OrderStatusHistories.Queries.GetAllHistoriesForAdmin
{
    public sealed class GetAllHistoriesForAdminQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllHistoriesForAdminQuery, PagedResult<GetHistoryDto>>
    {
        public async Task<PagedResult<GetHistoryDto>> Handle(GetAllHistoriesForAdminQuery request, CancellationToken cts)
        {
            var pagedResult = await unitOfWork.Repository<Order>()
                .GetQueryable()
                .AsNoTracking()
                .OrderByDescending(c => c.Id)
                .ProjectTo<GetHistoryDto>(mapper.ConfigurationProvider)
                .ToPagedListAsync(request.PageNumber, request.PageSize, cts);

            return pagedResult;
        }
    }
}
