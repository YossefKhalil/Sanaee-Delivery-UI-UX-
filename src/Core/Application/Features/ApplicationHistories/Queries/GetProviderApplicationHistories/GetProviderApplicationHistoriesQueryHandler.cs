

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.ApplicationHistories.Dtos;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.ApplicationHistories.Queries.GetProviderApplicationHistories
{
    public sealed class GetProviderApplicationHistoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetProviderApplicationHistoriesQuery, IReadOnlyList<GetApplicationHistoriesDto>>
    {
        public async Task<IReadOnlyList<GetApplicationHistoriesDto>> Handle(GetProviderApplicationHistoriesQuery request, CancellationToken cts)
        {
            var histories = await unitOfWork.Repository<ApplicationHistory>()
                                            .GetQueryable()
                                            .AsNoTracking()
                                            .ProjectTo<GetApplicationHistoriesDto>(mapper.ConfigurationProvider)
                                            .Where(h => h.ProviderId == request.ProviderID)
                                            .ToListAsync(cts);

            if (histories.Count == 0)
                throw new NotFoundException(nameof(ApplicationHistory), request.ProviderID);

            return histories;
        }
    }
}
