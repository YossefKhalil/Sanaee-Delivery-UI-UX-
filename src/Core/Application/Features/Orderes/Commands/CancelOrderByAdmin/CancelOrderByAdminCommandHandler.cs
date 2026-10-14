
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Orderes.Commands.CancelOrderByAdmin
{
    public sealed class CancelOrderByAdminCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CancelOrderByAdminCommand>
    {
        public async Task Handle(CancelOrderByAdminCommand request, CancellationToken cts)
        {
            var order =
                await unitOfWork.Repository<Order>().GetByIdAsync(request.OrderId, cts);

            if (order is null)
                throw new NotFoundException(nameof(Order), request.OrderId);

            order.CancelByAdmin(request.ReasonId, request.IssueRefund);

            var orderStatusHistory = new OrderStatusHistory
            {
                Status = order.Status,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow,
                UserId = order.UserId,
                OrderId = request.OrderId,
                ReasonId = request.ReasonId,
            };

            await unitOfWork.Repository<OrderStatusHistory>().AddAsync(orderStatusHistory, cts);

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
