using Core.Application.Common.Contracts;
using Core.Application.Common.Models;
using Domain.Events;
using MediatR;


namespace Core.Application.Features.Orderes.EventHandlers.OrderCancelledByAdmin
{
    public class RefundPaymentHandler : INotificationHandler<DomainEventNotification<OrderCancelledByAdminEvent>>
    {
        private readonly IPaymentService _paymentService;

        public RefundPaymentHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Handle(DomainEventNotification<OrderCancelledByAdminEvent> notification, CancellationToken cancellationToken)
        {
            // الوصول للحدث الأصلي عن طريق notification.DomainEvent
            var domainEvent = notification.DomainEvent;

            // يتفاعل فقط إذا كان الأدمن حدد خيار الاسترداد
            if (domainEvent.IssueRefund)
            {
                await _paymentService.RefundOrderAsync(domainEvent.OrderID, domainEvent.UserId);
            }
        }
    }
}
