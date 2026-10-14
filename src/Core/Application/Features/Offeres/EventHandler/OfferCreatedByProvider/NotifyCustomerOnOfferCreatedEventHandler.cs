using Core.Application.Common.Contracts;
using Core.Application.Common.Models;
using Domain.Events;
using MediatR;

namespace Application.Features.Offeres.EventHandler.OfferCreatedByProvider
{
    public class NotifyCustomerOnOfferCreatedEventHandler
        : INotificationHandler<DomainEventNotification<OfferCreatedEvent>>
    {
        private readonly INotificationService _notificationService;

        public NotifyCustomerOnOfferCreatedEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task Handle(DomainEventNotification<OfferCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            await _notificationService.SendPushNotificationAsync(
                    userId: domainEvent.UserId,
                    Title: "Offer Created",
                    Message: $"A Provider create an offer #{domainEvent.Offer.Id}."
                );
        }
    }
}
