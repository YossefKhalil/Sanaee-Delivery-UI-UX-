
using AutoMapper;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.Categories.Dtos;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Categories.Queries.GetCategoryById
{
    public sealed class GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, GetCategoryDto>
    {
        public async Task<GetCategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cts)
        {
            var category = 
                await unitOfWork.Repository<Category>().GetByIdAsync(request.CategoryId, cts);

            if (category is null)
                throw new NotFoundException(nameof(Category), request.CategoryId);

            return mapper.Map<GetCategoryDto>(category);
        }
    }
}
