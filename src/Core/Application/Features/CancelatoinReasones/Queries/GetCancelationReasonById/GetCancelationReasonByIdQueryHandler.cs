
using Application.Features.CancelatoinReasones.Dto;
using AutoMapper;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.CancelatoinReasones.Queries.GetCancelationReasonById
{
    public sealed class GetCancelationReasonByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetCancelationReasonByIdQuery, GetCancelationReasonByIdDto>
    {
        public async Task<GetCancelationReasonByIdDto> Handle(GetCancelationReasonByIdQuery request, CancellationToken cts)
        {
            var cancelationReason = 
                await unitOfWork.Repository<CancelationReason>().GetByIdAsync(request.ReasonId, cts);

            if (cancelationReason is null)
                throw new NotFoundException(nameof(CancelationReason), request.ReasonId);

            return mapper.Map<GetCancelationReasonByIdDto>(cancelationReason);
        }
    }
}
