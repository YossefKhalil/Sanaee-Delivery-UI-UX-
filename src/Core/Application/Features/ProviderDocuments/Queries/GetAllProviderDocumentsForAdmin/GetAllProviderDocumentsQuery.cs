

using Core.Application.Features.ProviderDocuments.Dtos;
using MediatR;

namespace Core.Application.Features.ProviderDocuments.Queries.GetAllProviderDocuments
{
    /// <summary>
    /// Get all Provider Document's to show and check it by admin
    /// </summary>
    public sealed record GetAllProviderDocumentsQuery(int ProviderId) : IRequest<IReadOnlyList<GetDocumentDto>>;
    
}
