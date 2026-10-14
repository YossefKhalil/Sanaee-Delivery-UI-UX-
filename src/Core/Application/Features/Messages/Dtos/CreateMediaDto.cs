using Core.Domain.Enums;

namespace Core.Application.Features.Messages.Dtos
{
    public class CreateMediaDto
    {
        public int RecipientId { get; set; }
        public int OrderId { get; set; }
        public MessageType Type { get; set; }
        public string AttachmentUrl { get; set; } = string.Empty;
        public string? Content { get; set; }
    }
}
