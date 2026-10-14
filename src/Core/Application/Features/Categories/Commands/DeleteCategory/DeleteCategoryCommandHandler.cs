

using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Categories.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryCommand>
    {
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cts)
        {
            var category = 
                await unitOfWork.Repository<Category>().GetByIdAsync(request.CategoryId, cts);

            if (category is null)
                throw new NotFoundException(nameof(Category), request.CategoryId);

            unitOfWork.Repository<Category>().Delete(category);
            await unitOfWork.CompleteAsync(cts);
        }
    }
}
