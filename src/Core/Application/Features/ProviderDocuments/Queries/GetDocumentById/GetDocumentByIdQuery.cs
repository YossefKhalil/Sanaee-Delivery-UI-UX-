
using Core.Application.Features.ProviderDocuments.Dtos;
using MediatR;

namespace Core.Application.Features.ProviderDocuments.Queries.GetDocumentById
{
    /// <summary>
    /// Getting Document By id for admin to show and check it
    /// </summary>
    /// <param name="DocumentId"></param>
    public sealed record GetDocumentByIdQuery(int DocumentId) : IRequest<GetDocumentDto>;

}
