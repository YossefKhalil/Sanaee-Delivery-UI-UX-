

using Core.Domain.Common;

namespace Core.Domain.Events
{
    public record OrderCancelledByCustomerEvent(int OrderId, int UserId, int? ProviderId, int ReasonId) : IDomainEvent;
    
}
