
using Core.Domain.Enums;
using MediatR;

namespace Core.Application.Features.ProviderDocuments.Commands.ChangeDocumentStatus
{
    public sealed record ChangeDocumentStatusCommand(int DocumentId, DocumentStatus status, int AdminId) : IRequest;
    
    
}
