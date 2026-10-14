using Core.Application.Common.Contracts;
using Core.Application.Common.Models;
using Core.Domain.Events;
using MediatR;

namespace Application.Features.Orderes.EventHandlers.OrderCancelledByProvider
{
    public class ApplyProviderPenaltyEventHandler
    : INotificationHandler<DomainEventNotification<OrderCancelledByProviderEvent>>
    {
        private readonly IProviderPenaltyService _penaltyService;

        public ApplyProviderPenaltyEventHandler(IProviderPenaltyService penaltyService)
        {
            _penaltyService = penaltyService;
        }

        public async Task Handle(
            DomainEventNotification<OrderCancelledByProviderEvent> notification,
            CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            // Apply a fine for cancellation
            await _penaltyService.ApplyPenaltyForCancellationAsync(
                ProviderId: domainEvent.ProviderId,
                OrderId: domainEvent.OrderId,
                ReasonId: domainEvent.ReasonId
            );
        }
    }
}
