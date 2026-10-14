

using Core.Domain.Enums;

namespace Core.Application.Features.ProviderDocuments.Dtos
{
    public class ProviderDocumentDto
    {
        public DocumentType Type { get; set; }
        public string DocumentUrl { get; set; } = string.Empty;
    }
}
