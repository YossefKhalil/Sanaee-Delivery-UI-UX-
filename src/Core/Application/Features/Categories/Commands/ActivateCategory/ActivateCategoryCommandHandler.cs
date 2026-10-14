using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.Categories.Commands.ActivateCategory;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Categories.Commands.ActivateCategory
{
    public class ActivateCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ActivateCategoryCommand>
    {
        public async Task Handle(ActivateCategoryCommand request, CancellationToken cts)
        {
            var category = await unitOfWork.Repository<Category>()
                                           .GetQueryable()
                                           .IgnoreQueryFilters()
                                           .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cts);

            if (category is null)
                throw new NotFoundException(nameof(Category), request.CategoryId);

            category.Unfreeze();

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
