using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Orderes.Commands.CreateOrder
{
    public sealed class CreatedOrderCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatedOrderCommand, int>
    {
        public async Task<int> Handle(CreatedOrderCommand request, CancellationToken cts)
        {
            var order = new Order()
            {
                Status = Domain.Enums.OrderStatus.Pending,
                VisitingTime = request.dto.VisitingTime,
                ProblemPhoto = request.dto.ProblemPhoto,
                ProblemDescription = request.dto.ProblemDescription,
                CategoryId = request.dto.CategoryId,
                AddressId = request.dto.AddressId,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                IsClientReviewed = false,
                IsProviderReviewed = false
            };

            await unitOfWork.Repository<Order>().AddAsync(order, cts);

            var orderStatusHistoy = new OrderStatusHistory
            {
                Status = order.Status,
                Notes = "The order created by the customer",
                CreatedAt = DateTime.UtcNow,
                UserId = request.UserId,
                Order = order
            };

            await unitOfWork.Repository<OrderStatusHistory>().AddAsync(orderStatusHistoy, cts);

            await unitOfWork.CompleteAsync(cts);

            return order.Id;
        }
    }
}
