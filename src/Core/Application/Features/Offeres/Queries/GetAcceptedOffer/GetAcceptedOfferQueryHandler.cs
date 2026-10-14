

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.Offeres.Dtos;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Offeres.Queries.GetAcceptedOffer
{
    public sealed class GetAcceptedOfferQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAcceptedOfferQuery, GetOfferDto>
    {
        public async Task<GetOfferDto> Handle(GetAcceptedOfferQuery request, CancellationToken cts)
        {
            var acceptedOffer = await unitOfWork.Repository<Offer>()
                                                .GetQueryable()
                                                .AsNoTracking()
                                                .Where(o => o.OrderId == request.OrderId)
                                                .ProjectTo<GetOfferDto>(mapper.ConfigurationProvider)
                                                .FirstOrDefaultAsync(cts);

            if (acceptedOffer is null)
                throw new NotFoundException("No offer accepted for this order.");

            return acceptedOffer;
        }
    }
}
