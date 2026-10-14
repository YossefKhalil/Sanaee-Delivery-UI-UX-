

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.ProviderCategories.Dtos;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Core.Application.Features.ProviderCategories.Queries.GetProviderCategoryById
{
    public sealed class GetProviderCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetProviderCategoryByIdQuery, GetProviderCategoryDto>
    {
        public async Task<GetProviderCategoryDto> Handle(GetProviderCategoryByIdQuery request, CancellationToken cts)
        {
            var providerCategory = await unitOfWork.Repository<ProviderCategory>()
                                                   .GetQueryable()
                                                   .AsNoTracking()
                                                   .Where(pc => pc.ProviderCategoryId == request.ProviderCategoryId)
                                                   .ProjectTo<GetProviderCategoryDto>(mapper.ConfigurationProvider)
                                                   .FirstOrDefaultAsync(cts);


            if (providerCategory is null)
                throw new NotFoundException(nameof(ProviderCategory), request.ProviderCategoryId);

            return providerCategory;
        }
    }
}
