using Core.Domain.Enums;


namespace Core.Application.Features.ProviderDocuments.Dtos
{
    // Returning this Dto to Admin when trying get Document by id
    public class GetDocumentDto
    {
        public int DocumentId { get; set; }
        public DocumentType Type { get; set; }
        public DocumentStatus Status { get; set; }
        public string DocumentUrl { get; set; } = string.Empty;
        public int ProviderId { get; set; }
        public int ChangedBy { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
