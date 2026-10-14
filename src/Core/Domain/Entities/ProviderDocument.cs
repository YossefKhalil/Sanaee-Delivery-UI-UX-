using Core.Domain.Enums;

namespace Core.Domain.Entities
{
    public class ProviderDocument
    {
        public int DocumentId { get; set; }
        public DocumentType Type { get; set; }
        public DocumentStatus Status { get; set; }
        public string DocumentUrl { get; set; } = string.Empty;


        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = null!;


        public int? ChangedBy { get; set; }
        public User? User { get; set; }
    }
}
