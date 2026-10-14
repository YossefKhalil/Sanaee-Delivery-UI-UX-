using Core.Application.Common.Contracts;
using Core.Application.Common.Models;
using Core.Domain.Enums;
using Core.Domain.Events;
using MediatR;

namespace Core.Application.Features.Messages.EventHandlers
{
    public class MessageSentPushNotificationHandler : INotificationHandler<DomainEventNotification<MessageSentDomainEvent>>
    {
        private readonly INotificationService _notification;

        public MessageSentPushNotificationHandler(INotificationService notification)
        {
            _notification = notification;
        }


        public async Task Handle(DomainEventNotification<MessageSentDomainEvent> notification, CancellationToken cancellationToken)
        {
            var message = notification.DomainEvent.Message;

            // إرسال إشعار Push للمستقبل لو كان التطبيق مغلقاً
            await _notification.SendPushNotificationAsync(
                userId: message.RecipientId,
                Title: "رسالة جديدة",
                Message: message.Type == MessageType.VoiceNote ? "🎤 تسجيل صوتي جديد" : message.Content ?? string.Empty
            );
        }
    }
}
