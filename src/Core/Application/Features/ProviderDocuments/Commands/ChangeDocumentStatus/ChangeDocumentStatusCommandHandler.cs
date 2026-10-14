

using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.ProviderDocuments.Commands.ChangeDocumentStatus
{

    // Handler to change document status by admin
    public sealed class ChangeDocumentStatusCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangeDocumentStatusCommand>
    {
        public async Task Handle(ChangeDocumentStatusCommand request, CancellationToken cts)
        {
            var document = await unitOfWork.Repository<ProviderDocument>()
                                            .GetQueryable()
                                            .FirstOrDefaultAsync(d => d.DocumentId == request.DocumentId &&
                                                                      d.ChangedBy == request.AdminId, cts);


            if (document is null)
                throw new NotFoundException(nameof(ProviderDocument), request.DocumentId);


            document.Status = request.status;
            document.ChangedBy = request.AdminId;


            await unitOfWork.CompleteAsync(cts);
        }
    }
}
