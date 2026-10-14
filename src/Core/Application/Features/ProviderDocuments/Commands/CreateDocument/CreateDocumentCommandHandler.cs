

using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;

namespace Core.Application.Features.ProviderDocuments.Commands.CreateDocument
{
    public sealed class CreateDocumentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateDocumentCommand, int>
    {
        public async Task<int> Handle(CreateDocumentCommand request, CancellationToken cts)
        {
            var document = new ProviderDocument
            {
                Status = DocumentStatus.Pending,
                Type = request.dto.Type,
                DocumentUrl = request.dto.DocumentUrl,
                ProviderId = request.ProviderId
            };

            await unitOfWork.Repository<ProviderDocument>().AddAsync(document, cts);

            await unitOfWork.CompleteAsync(cts);

            return document.DocumentId;
        }
    }
}
