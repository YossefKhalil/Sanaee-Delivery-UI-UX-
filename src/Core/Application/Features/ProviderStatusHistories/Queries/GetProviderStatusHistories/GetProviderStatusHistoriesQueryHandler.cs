

using Core.Application.Features.ProviderStatusHistories.Dto;
using Core.Application.Common.Contracts.Persistence;
using AutoMapper;
using MediatR;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Application.Common.Exceptions;
using AutoMapper.QueryableExtensions;

namespace Core.Application.Features.ProviderStatusHistories.Queries.GetProviderStatusHistories
{
    public sealed class GetProviderStatusHistoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetProviderStatusHistoriesQuery, IReadOnlyList<GetProviderStatusHistoriesDto>>
    {
        public async Task<IReadOnlyList<GetProviderStatusHistoriesDto>> Handle(GetProviderStatusHistoriesQuery request, CancellationToken cts)
        {
            var histories = await unitOfWork.Repository<ProviderStatusHistory>()
                                            .GetQueryable()
                                            .AsNoTracking()
                                            .ProjectTo<GetProviderStatusHistoriesDto>(mapper.ConfigurationProvider)
                                            .Where(h => h.ProviderId == request.ProviderId)
                                            .ToListAsync(cts);

            if (histories.Count == 0)
                throw new NotFoundException(nameof(ProviderStatusHistory), request.ProviderId);

            return histories;
        }
    }
}
