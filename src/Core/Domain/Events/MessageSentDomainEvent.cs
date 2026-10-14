using Core.Domain.Entities;
using Core.Domain.Common;

namespace Core.Domain.Events
{
    public record MessageSentDomainEvent(Message Message) : IDomainEvent;
}
