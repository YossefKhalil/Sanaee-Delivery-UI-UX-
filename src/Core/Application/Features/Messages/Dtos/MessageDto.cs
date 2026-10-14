

using Core.Domain.Entities;
using Core.Domain.Enums;

namespace Core.Application.Features.Messages.Dtos
{
    public class MessageDto
    {
        public MessageDto(Message message)
        {
            Content = message.Content;
            Type = message.Type;
            AttachmentUrl = message.AttachmentUrl;
            SentAt = message.SentAt;
            IsSystemMessage = message.IsSystemMessage;
            SenderId = message.SenderId;
            FirstName = message.Sender.FirstName;
            LastName = message.Sender.LastName;
            OrderId = message.OrderId;
        }

        public string? Content { get; private set; }
        public MessageType Type { get; private set; }
        public string? AttachmentUrl { get; private set; }
        public DateTime SentAt { get; private set; }
        public bool IsSystemMessage { get; private set; }


        public int SenderId { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;

        public int OrderId { get; private set; }
    }
}
