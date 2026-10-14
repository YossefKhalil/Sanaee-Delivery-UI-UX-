using Core.Application.Common.Contracts;
using Core.Application.Common.Models;
using Core.Domain.Events;
using MediatR;

namespace Application.Features.Offeres.EventHandler.OfferAcceptedByCustomer
{
    public class NotifyProviderOnOfferAcceptedEventHandler
        : INotificationHandler<DomainEventNotification<OfferAcceptedEvent>>
    {
        private readonly INotificationService _notificationService;

        public NotifyProviderOnOfferAcceptedEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task Handle(DomainEventNotification<OfferAcceptedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            await _notificationService.SendPushNotificationAsync(
                    userId: domainEvent.ProviderId,
                    Title: "Offer Accepted",
                    Message: $"Customer Accept the offer #{domainEvent.OfferId}."
                );
        }
    }
}
