

using Core.Application.Common.Contracts.Persistence;
using Core.Application.Features.Provideres.Dtos;
using Core.Application.Common.Extentions;
using Core.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;
using MediatR;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Core.Application.Features.Provideres.Queries.GetAllProviders
{
    public class GetAllProvidersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllProvidersQuery, PagedResult<ProviderDto>>
    {

        public async Task<PagedResult<ProviderDto>> Handle(GetAllProvidersQuery request, CancellationToken cts)
        {
            var pagedResult = await unitOfWork.Repository<Provider>()
                .GetQueryable()
                .AsNoTracking()
                .OrderByDescending(c => c.ProviderId)
                .ProjectTo<ProviderDto>(mapper.ConfigurationProvider)
                .ToPagedListAsync(request.PageNumber, request.PageSize, cts);

            return pagedResult;
        }
    }
}
