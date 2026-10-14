

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Features.CancelatoinReasones.Dto;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.CancelatoinReasones.Queries.GetAdminCancelationReasons
{
    public sealed class GetAdminCancelationReasonsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAdminCancelationReasonsQuery, IReadOnlyList<GetUserCancelationReasonDto>>
    {
        async Task<IReadOnlyList<GetUserCancelationReasonDto>> IRequestHandler<GetAdminCancelationReasonsQuery, IReadOnlyList<GetUserCancelationReasonDto>>.Handle(GetAdminCancelationReasonsQuery request, CancellationToken cts)
        {
            var result = await unitOfWork.Repository<CancelationReason>()
                                         .GetQueryable()
                                         .AsNoTracking()
                                         .Where(cr => cr.ApplicableTo == UserType.Admin)
                                         .ProjectTo<GetUserCancelationReasonDto>(mapper.ConfigurationProvider)
                                         .ToListAsync(cts);

            return result;
        }
    }
}
