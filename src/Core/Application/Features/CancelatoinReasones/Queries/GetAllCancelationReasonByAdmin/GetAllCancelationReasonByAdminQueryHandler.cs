
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Features.CancelatoinReasones.Dto;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.CancelatoinReasones.Queries.GetAllCancelationReasonByAdmin
{
    public sealed class GetAllCancelationReasonByAdminQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCancelationReasonByAdminQuery, IReadOnlyList<CancelationReason>>
    {
        public async Task<IReadOnlyList<CancelationReason>> Handle(GetAllCancelationReasonByAdminQuery request, CancellationToken cts)
        {
            var result = await unitOfWork.Repository<CancelationReason>()
                                         .GetQueryable()
                                         .AsNoTracking()
                                         .IgnoreQueryFilters()
                                         .ToListAsync(cts);

            return result;
        }
    }
}
