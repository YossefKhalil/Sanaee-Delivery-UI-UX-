using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Features.Offeres.Dtos;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Offeres.Queries.GetAllOffersForOrder
{
    public sealed class GetAllOffersForOrderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllOffersForOrderQuery, IReadOnlyList<GetAllOffersForOrderDto>>
    {
        public async Task<IReadOnlyList<GetAllOffersForOrderDto>> Handle(GetAllOffersForOrderQuery request, CancellationToken cts)
        {
            var offers = await unitOfWork.Repository<Offer>()
                                         .GetQueryable()
                                         .AsNoTracking() 
                                         .Where(o => o.OrderId == request.OrderId)
                                         .ProjectTo<GetAllOffersForOrderDto>(mapper.ConfigurationProvider)
                                         .ToListAsync(cts);

            return offers;
        }
    }
}
