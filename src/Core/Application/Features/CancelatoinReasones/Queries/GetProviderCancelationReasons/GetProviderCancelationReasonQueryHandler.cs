using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Features.CancelatoinReasones.Dto;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.CancelatoinReasones.Queries.GetProviderCancelationReasons
{
    public sealed class GetProviderCancelationReasonQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetProviderCancelationReasonQuery, IReadOnlyList<GetUserCancelationReasonDto>>
    {
        async Task<IReadOnlyList<GetUserCancelationReasonDto>> IRequestHandler<GetProviderCancelationReasonQuery, IReadOnlyList<GetUserCancelationReasonDto>>.Handle(GetProviderCancelationReasonQuery request, CancellationToken cts)
        {
            var result = await unitOfWork.Repository<CancelationReason>()
                                         .GetQueryable()
                                         .AsNoTracking()
                                         .Where(cr => cr.ApplicableTo == UserType.Provider)
                                         .ProjectTo<GetUserCancelationReasonDto>(mapper.ConfigurationProvider)
                                         .ToListAsync(cts);

            return result;
        }
    }
}
