
using Core.Application.Features.ProviderDocuments.Dtos;
using MediatR;

namespace Core.Application.Features.ProviderDocuments.Commands.CreateDocument
{
    public sealed record CreateDocumentCommand
        (

            ProviderDocumentDto dto,
            int ProviderId

        ) : IRequest<int>;
    
}
