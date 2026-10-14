

using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Categories.Commands.CreateCategory
{
    public sealed class CreateCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, int>
    {
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cts)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                IconUrl = request.IconUrl,
                IsActive = true,
                ParentCategoryId = request.ParentCategoryId,
                IsDeleted = false
            };

            await unitOfWork.Repository<Category>().AddAsync(category, cts);
            await unitOfWork.CompleteAsync(cts);

            return category.CategoryId;
        }
    }
}
