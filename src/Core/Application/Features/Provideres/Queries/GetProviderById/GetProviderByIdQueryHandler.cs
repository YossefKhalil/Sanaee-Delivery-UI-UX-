

using AutoMapper;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.Provideres.Dtos;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Provideres.Queries.GetProviderById
{
    public sealed class GetProviderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetProviderByIdQuery, ProviderDto>
    {
        public async Task<ProviderDto> Handle(GetProviderByIdQuery request, CancellationToken cts)
        {
            var provider = await unitOfWork.Repository<Provider>().GetByIdAsync(request.ProviderId, cts);

            if (provider is null || request.ProviderId != provider.ProviderId)
                throw new NotFoundException(nameof(Provider), request.ProviderId);

            return mapper.Map<ProviderDto>(provider);
        }
    }
}
