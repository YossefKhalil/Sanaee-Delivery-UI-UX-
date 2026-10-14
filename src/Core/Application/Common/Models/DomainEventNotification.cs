

using Core.Domain.Common;
using MediatR;

namespace Core.Application.Common.Models
{
    public class DomainEventNotification<TDomainEvent> : INotification
    where TDomainEvent : IDomainEvent
    {
        public TDomainEvent DomainEvent { get; }

        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}
