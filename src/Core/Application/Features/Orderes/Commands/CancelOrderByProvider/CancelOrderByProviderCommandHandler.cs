
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Orderes.Commands.CancelOrderByProvider
{
    public sealed class CancelOrderByProviderCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CancelOrderByProviderCommand>
    {
        public async Task Handle(CancelOrderByProviderCommand request, CancellationToken cts)
        {
            var order =
                await unitOfWork.Repository<Order>().GetByIdAsync(request.OrderId, cts);

            if (order is null)
                throw new NotFoundException(nameof(Order), request.OrderId);

            order.CancelByProvider(request.ReasonId);

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
