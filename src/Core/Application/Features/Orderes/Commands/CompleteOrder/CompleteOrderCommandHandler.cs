

using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Orderes.Commands.CompleteOrder
{
    public sealed class CompleteOrderCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CompleteOrderCommand>
    {
        public async Task Handle(CompleteOrderCommand request, CancellationToken cts)
        {
            var order =
                await unitOfWork.Repository<Order>().GetByIdAsync(request.OrderId, cts);

            if (order is null)
                throw new NotFoundException(nameof(Order), request.OrderId);

            order.Complete();

            var orderStatusHistory = new OrderStatusHistory
            {
                Status = order.Status,
                CreatedAt = DateTime.UtcNow,
                UserId = order.UserId,
                OrderId = request.OrderId,
            };

            await unitOfWork.Repository<OrderStatusHistory>().AddAsync(orderStatusHistory, cts);

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
