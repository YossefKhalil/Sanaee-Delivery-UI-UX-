


using Core.Domain.Entities;
using Core.Domain.Common;

namespace Domain.Events
{
    public record OfferCreatedEvent(Offer Offer, int UserId, int ProviderId) : IDomainEvent;
}
