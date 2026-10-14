
using AutoMapper;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.OrderStatusHistories.Dtos;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.OrderStatusHistories.Queries.GetHistoryByIdForAdmin
{
    public class GetHistoryByIdForAdminQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetHistoryByIdForAdminQuery, GetHistoryDto>
    {
        public async Task<GetHistoryDto> Handle(GetHistoryByIdForAdminQuery request, CancellationToken cts)
        {
            var history = await unitOfWork.Repository<OrderStatusHistory>().GetByIdAsync(request.HistoryId, cts);

            if (history is null)
                throw new NotFoundException(nameof(OrderStatusHistory), request.HistoryId);

            return mapper.Map<GetHistoryDto>(history);
        }
    }
}
