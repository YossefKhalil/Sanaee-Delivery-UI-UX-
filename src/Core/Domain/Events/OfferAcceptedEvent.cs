using Core.Domain.Common;

namespace Core.Domain.Events
{
    public record OfferAcceptedEvent(int OfferId, int OrderId, int UserId, int ProviderId) : IDomainEvent;
}
