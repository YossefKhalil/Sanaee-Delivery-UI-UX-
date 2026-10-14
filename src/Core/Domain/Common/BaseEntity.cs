namespace Core.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        private readonly List<IDomainEvent> _domainEvents = [];

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        // Add Event
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        // Remove Events after firing
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
