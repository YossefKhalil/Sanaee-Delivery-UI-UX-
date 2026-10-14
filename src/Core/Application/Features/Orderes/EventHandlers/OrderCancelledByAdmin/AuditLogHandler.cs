using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Models;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Domain.Events;
using MediatR;


namespace Core.Application.Features.Orderes.EventHandlers.OrderCancelledByAdmin
{
    public class AuditLogHandler : INotificationHandler<DomainEventNotification<OrderCancelledByAdminEvent>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditLogHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DomainEventNotification<OrderCancelledByAdminEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            var Reason = await _unitOfWork.Repository<CancelationReason>().GetByIdAsync(domainEvent.ReasonId, cancellationToken);

            var log = new AuditLog
            {
                EntityName = EntityName.Order,
                Action = ActionType.Canellation,
                EntityId = domainEvent.OrderID,
                Reason = Reason!.Text,
                CreatedDate = DateTime.UtcNow,
                PerformedById = domainEvent.UserId,
                IsSelfAction = false,
            };

            await _unitOfWork.Repository<AuditLog>().AddAsync(log, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
    }
}
