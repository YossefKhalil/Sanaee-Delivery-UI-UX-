
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Categories.Commands.UpdateCategory
{
    public sealed class UpdateCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand>
    {
        public async Task Handle(UpdateCategoryCommand request, CancellationToken cts)
        {
            var category = await unitOfWork.Repository<Category>().GetByIdAsync(request.CategoryId, cts);


            if (category is null)
                throw new NotFoundException(nameof(Category), request.CategoryId);

            if (!string.IsNullOrWhiteSpace(request.dto.Name))
                category.Name = request.dto.Name;

            if (!string.IsNullOrWhiteSpace(request.dto.Description))
                category.Description = request.dto.Description;

            if (!string.IsNullOrWhiteSpace(request.dto.IconUrl))
                category.IconUrl = request.dto.IconUrl;

            category.ParentCategoryId = request.dto.ParentCategoryId ?? category.ParentCategoryId;


            await unitOfWork.CompleteAsync(cts);
        }
    }
}
