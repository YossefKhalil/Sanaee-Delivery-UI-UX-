using Core.Application.Common.Contracts;
using Core.Application.Common.Models;
using Core.Domain.Events;
using MediatR;

namespace Core.Application.Features.Orderes.EventHandlers.OrderCancelledByCustomer
{
    public class ProcessCustomerRefundEventHandler
        : INotificationHandler<DomainEventNotification<OrderCancelledByCustomerEvent>>
    {

        private readonly IPaymentService _paymentService;

        public ProcessCustomerRefundEventHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Handle(DomainEventNotification<OrderCancelledByCustomerEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            // Call payment service
            await _paymentService.ProcessCustomerCancellationRefundAsync(
                OrderId: domainEvent.OrderId,
                CustomerId: domainEvent.UserId
            );
        }
    }
}
