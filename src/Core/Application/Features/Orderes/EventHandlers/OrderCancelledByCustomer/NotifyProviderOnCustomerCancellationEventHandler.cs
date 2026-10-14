using Core.Application.Common.Contracts;
using Core.Application.Common.Models;
using Core.Domain.Events;
using Domain.Events;
using MediatR;

namespace Core.Application.Features.Orderes.EventHandlers.OrderCancelledByCustomer
{
    public class NotifyProviderOnCustomerCancellationEventHandler
        : INotificationHandler<DomainEventNotification<OrderCancelledByCustomerEvent>>
    {

        private readonly INotificationService _notificationService;

        public NotifyProviderOnCustomerCancellationEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(DomainEventNotification<OrderCancelledByCustomerEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            // If there is a provider
            if (domainEvent.ProviderId.HasValue)
            {
                await _notificationService.SendPushNotificationAsync(
                    userId: domainEvent.ProviderId.Value,
                    Title: "Order Cancelled",
                    Message: $"Customer cancelled the order #{domainEvent.OrderId}."
                );
            }
        }
    }
}
