using Core.Application.Common.Contracts;
using Core.Application.Common.Models;
using Domain.Events;
using MediatR;


namespace Core.Application.Features.Orderes.EventHandlers.OrderCancelledByAdmin
{
    public class NotifyBothPartiesHandler : INotificationHandler<DomainEventNotification<OrderCancelledByAdminEvent>>
    {
        private readonly INotificationService _notificationService;

        public NotifyBothPartiesHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(DomainEventNotification<OrderCancelledByAdminEvent> notification, CancellationToken cancellationToken)
        {
            // Notify customer
            await _notificationService.SendPushAsync(
                UserId: notification.DomainEvent.UserId,
                Title: "Update your order",
                Message: $"Order #{notification.DomainEvent.OrderID} cancelled by admin."
            );

            // Notify provider
            if (notification.DomainEvent.ProviderId.HasValue)
            {
                await _notificationService.SendPushAsync(
                    UserId: notification.DomainEvent.ProviderId.Value,
                    Title: "Update the order",
                    Message: $"Order #{notification.DomainEvent.OrderID} cancelled by admin."
                );

            }
        }
    }
}