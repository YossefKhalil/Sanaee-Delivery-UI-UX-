

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Features.Orderes.Dtos;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Orderes.Queries.GetAllOrdersByCustomer
{
    internal class GetAllOrdersByCustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllOrdersByCustomerQuery, IReadOnlyList<GetAllOrdersForCustomerDto>>
    {
        public async Task<IReadOnlyList<GetAllOrdersForCustomerDto>> Handle(GetAllOrdersByCustomerQuery request, CancellationToken cts)
        {
            var orders = await unitOfWork.Repository<Order>()
                                         .GetQueryable()
                                         .AsNoTracking()
                                         .Where(
                                                  o =>
                                                      o.UserId == request.CustomerId &&
                                                      (o.Status != OrderStatus.Cancelled || o.Status != OrderStatus.Completed))
                                         .ProjectTo<GetAllOrdersForCustomerDto>(mapper.ConfigurationProvider)
                                         .ToListAsync(cts);

            return orders;
        }
    }
}
