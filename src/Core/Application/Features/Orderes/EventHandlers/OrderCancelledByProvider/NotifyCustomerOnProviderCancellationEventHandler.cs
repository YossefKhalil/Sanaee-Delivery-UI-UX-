

using Core.Application.Common.Contracts;
using Core.Application.Common.Models;
using Core.Domain.Events;
using MediatR;

namespace Core.Application.Features.Orderes.EventHandlers.OrderCancelledByProvider
{
    public class NotifyCustomerOnProviderCancellationEventHandler
        : INotificationHandler<DomainEventNotification<OrderCancelledByProviderEvent>>
    {
        private readonly INotificationService _notificationService;

        public NotifyCustomerOnProviderCancellationEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task Handle(DomainEventNotification<OrderCancelledByProviderEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            await _notificationService.SendPushNotificationAsync(
                userId: domainEvent.UserId,
                Title: "Excusing about the order",
                Message: $"The provider Excusing to execute the order #{domainEvent.OrderId}. You can open the order to other providers."
            );
        }
    }
}
