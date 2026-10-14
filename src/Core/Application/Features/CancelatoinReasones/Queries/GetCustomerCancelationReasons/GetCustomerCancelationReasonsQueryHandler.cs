using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Features.CancelatoinReasones.Dto;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Core.Application.Features.CancelatoinReasones.Queries.GetCustomerCancelationReasons
{
    public sealed class GetCustomerCancelationReasonsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetCustomerCancelationReasonsQuery, IReadOnlyList<GetUserCancelationReasonDto>>
    {
        async Task<IReadOnlyList<GetUserCancelationReasonDto>> IRequestHandler<GetCustomerCancelationReasonsQuery, IReadOnlyList<GetUserCancelationReasonDto>>.Handle(GetCustomerCancelationReasonsQuery request, CancellationToken cts)
        {
            var result = await unitOfWork.Repository<CancelationReason>()
                                         .GetQueryable()
                                         .AsNoTracking()
                                         .Where(cr => cr.ApplicableTo == UserType.Customer)
                                         .ProjectTo<GetUserCancelationReasonDto>(mapper.ConfigurationProvider)
                                         .ToListAsync(cts);

            return result;
        }
    }
}
