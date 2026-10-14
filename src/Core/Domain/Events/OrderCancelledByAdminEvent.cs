
using Core.Domain.Common;

namespace Domain.Events
{
    public record OrderCancelledByAdminEvent(int OrderID, int UserId, int? ProviderId, int ReasonId, bool IssueRefund) : IDomainEvent;
}
