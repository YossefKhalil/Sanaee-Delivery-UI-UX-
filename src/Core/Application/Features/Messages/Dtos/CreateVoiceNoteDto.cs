namespace Core.Application.Features.Messages.Dtos
{
    public class CreateVoiceNoteDto
    {
        public int RecipientId { get; set; }
        public int OrderId { get; set; }
        public string AttachmentUrl { get; set; } = string.Empty;
        public int DurationInSeconds { get; set; }
    }
}
