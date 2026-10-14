using Core.Domain.Enums;

namespace Core.Application.Features.Messages.Dtos
{
    public class GetMessageDto
    {
        public string? Content { get; private set; }
        public MessageType Type { get; private set; }
        public string? AttachmentUrl { get; private set; }
        public DateTime SentAt { get; private set; }
        public DateTime? ReadAt { get; private set; }
        public int? DurationInSeconds { get; private set; }
        public bool IsSystemMessage { get; private set; }
        public int SenderId { get; private set; }
        public int RecipientId { get; private set; }
    }
}
