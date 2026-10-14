
namespace Core.Application.Features.Messages.Dtos
{
    public class CreateTextDto
    {
        public int RecipientId { get; set; }
        public int OrderId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
