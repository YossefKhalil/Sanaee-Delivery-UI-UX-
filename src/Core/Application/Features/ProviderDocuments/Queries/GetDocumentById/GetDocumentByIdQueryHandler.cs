
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.ProviderDocuments.Dtos;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.ProviderDocuments.Queries.GetDocumentById
{
    
    public sealed class GetDocumentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetDocumentByIdQuery, GetDocumentDto>
    {
        public async Task<GetDocumentDto> Handle(GetDocumentByIdQuery request, CancellationToken cts)
        {
            var document =
                await unitOfWork.Repository<ProviderDocument>()
                                .GetQueryable()
                                .AsNoTracking()
                                .ProjectTo<GetDocumentDto>(mapper.ConfigurationProvider)
                                .FirstOrDefaultAsync(d => d.DocumentId == request.DocumentId, cts);

            if (document is null)
                throw new NotFoundException(nameof(ProviderDocument), request.DocumentId);

            return mapper.Map<GetDocumentDto>(document);
        }
    }
}
