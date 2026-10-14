using Core.Application.Common.Models;
using Core.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace Persistence.Interceptors
{
    public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task DispatchDomainEvents(DbContext? context)
        {
            if (context == null) return;

            // 1. Get All entities that have events
            var entities = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity)
                .ToList();

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            // 2. Clear list to prevents repetting
            entities.ForEach(e => e.ClearDomainEvents());

            // 3. Publish events by MediatR
            foreach (var domainEvent in domainEvents)
            {
                // تغليف الـ Domain Event بالـ Wrapper ديناميكياً قبل النشر
                var notificationType = typeof(DomainEventNotification<>)
                    .MakeGenericType(domainEvent.GetType());

                var notification = Activator.CreateInstance(notificationType, domainEvent);

                if (notification != null)
                {
                    await mediator.Publish(notification);
                }
            }
        }
    }
}
