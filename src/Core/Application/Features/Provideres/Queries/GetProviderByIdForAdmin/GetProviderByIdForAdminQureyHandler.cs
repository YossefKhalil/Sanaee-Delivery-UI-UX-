using Core.Application.Features.Provideres.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Provideres.Queries.GetProviderByIdForAdmin
{
    public sealed class GetProviderByIdForAdminQureyHandler(IUnitOfWork unitOfWork, IMapper mapper) :
        IRequestHandler<GetProviderByIdForAdminQurey, GetProviderForAdminDto>
    {
        public async Task<GetProviderForAdminDto> Handle(GetProviderByIdForAdminQurey request, CancellationToken cts)
        {
            var provider = await unitOfWork.Repository<Provider>()
                                           .GetQueryable()
                                           .AsNoTracking()
                                           .ProjectTo<GetProviderForAdminDto>(mapper.ConfigurationProvider)
                                           .FirstOrDefaultAsync(p => p.ProviderId == request.ProviderId ,cts);

            if (provider is null)
                throw new NotFoundException(nameof(Provider), request.ProviderId);

            return provider;
        }
    }
}
