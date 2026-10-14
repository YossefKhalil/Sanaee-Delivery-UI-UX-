using Core.Application.Common.Contracts.Persistence;
using Core.Application.Features.Orderes.Dtos;
using Core.Application.Common.Exceptions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Core.Application.Features.Orderes.Queries.GetOrderById
{
    public sealed class GetOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetOrderByIdQuery, GetOrderDto>
    {
        public async Task<GetOrderDto> Handle(GetOrderByIdQuery request, CancellationToken cts)
        {
            var order = await unitOfWork.Repository<Order>()
                                        .GetQueryable()
                                        .AsNoTracking()
                                        .Where(o => o.Id == request.OrderId)
                                        .ProjectTo<GetOrderDto>(mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync(cts);


            if (order is null)
                throw new NotFoundException(nameof(Order), request.OrderId);

            return order;
        }
    }
}
