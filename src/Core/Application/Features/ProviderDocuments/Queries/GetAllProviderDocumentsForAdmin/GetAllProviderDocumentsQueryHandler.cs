
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.ProviderDocuments.Dtos;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.ProviderDocuments.Queries.GetAllProviderDocuments
{
    public sealed class GetAllProviderDocumentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllProviderDocumentsQuery, IReadOnlyList<GetDocumentDto>>
    {
        public async Task<IReadOnlyList<GetDocumentDto>> Handle(GetAllProviderDocumentsQuery request, CancellationToken cts)
        {
            var documents = await unitOfWork.Repository<ProviderDocument>()
                                      .GetQueryable()
                                      .AsNoTracking()
                                      .ProjectTo<GetDocumentDto>(mapper.ConfigurationProvider)
                                      .Where(d => d.ProviderId == request.ProviderId)
                                      .ToListAsync(cts);

            if (documents.Count == 0)
                throw new NotFoundException(nameof(ProviderDocument), request.ProviderId);

            return documents;
        }
    }
}
