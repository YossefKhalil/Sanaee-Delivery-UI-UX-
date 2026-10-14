
using AutoMapper;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.Offeres.Dtos;
using Core.Application.Features.Offeres.Queries.GetOfferById;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.Offeres.Queries.GetOfferById
{
    public sealed class GetOfferByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetOfferByIdQuery, GetOfferDto>
    {
        public async Task<GetOfferDto> Handle(GetOfferByIdQuery request, CancellationToken cts)
        {
            var offer = 
                await unitOfWork.Repository<Offer>().GetByIdAsync(request.OfferId, cts);

            if (offer is null)
                throw new NotFoundException(nameof(Offer), request.OfferId);

            return mapper.Map<GetOfferDto>(offer);
        }
    }
}
