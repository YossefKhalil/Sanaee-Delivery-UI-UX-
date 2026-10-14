
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Categories.Commands.DeactivateCategory
{
    public sealed class DeactivateCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeactivateCategoryCommand>
    {
        public async Task Handle(DeactivateCategoryCommand request, CancellationToken cts)
        {
            var category = await unitOfWork.Repository<Category>()
                                           .GetQueryable()
                                           .IgnoreQueryFilters()
                                           .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cts);

            if (category is null)
                throw new NotFoundException(nameof(Category), request.CategoryId);

            category.Freeze();

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
