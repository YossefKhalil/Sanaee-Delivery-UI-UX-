using Microsoft.AspNetCore.SignalR;
using MediatR;
using API.Hubs;
using Core.Application.Common.Models;
using Core.Application.Features.Messages.Dtos;
using Core.Domain.Events;

namespace Application.Features.Messages.EventHandlers
{
    public class MessageSentSignalRHandler : INotificationHandler<DomainEventNotification<MessageSentDomainEvent>>
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageSentSignalRHandler(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(DomainEventNotification<MessageSentDomainEvent> notification, CancellationToken cancellationToken)
        {
            var message = notification.DomainEvent.Message;

            // إرسال الرسالة لحظياً لأطراف المحادثة الموجودين أونلاين
            await _hubContext.Clients.Group($"Order_{message.OrderId}")
                .SendAsync("ReceiveMessage", new MessageDto(message), cancellationToken);

            
        }
    }
}
