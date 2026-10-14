
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Categories.Commands.RestoreCategory
{
    internal class RestoreCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RestoreCategoryCommand>
    {
        public async Task Handle(RestoreCategoryCommand request, CancellationToken cts)
        {
            var category = await unitOfWork.Repository<Category>()
                                           .GetQueryable()
                                           .IgnoreQueryFilters()
                                           .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cts);
                

            if (category is null)
                throw new NotFoundException(nameof(Category), request.CategoryId);

            category.IsDeleted = false;
            category.DeletedAt = null;

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
