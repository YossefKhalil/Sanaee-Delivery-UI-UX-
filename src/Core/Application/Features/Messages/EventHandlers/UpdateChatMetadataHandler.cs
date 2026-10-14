using Core.Application.Common.Contracts;
using Core.Application.Common.Models;
using Core.Domain.Events;
using MediatR;

namespace Application.Features.Messages.EventHandlers
{
    internal class UpdateChatMetadataHandler : INotificationHandler<DomainEventNotification<MessageSentDomainEvent>>
    {
        private readonly IAppDbContext _context;

        public UpdateChatMetadataHandler(IAppDbContext context) => _context = context;

        public async Task Handle(DomainEventNotification<MessageSentDomainEvent> notification, CancellationToken cancellationToken)
        {
            // تحديث أحدث رسالة في الطلب أو زيادة عداد الرسائل غير المقروءة
        }
    }
}
