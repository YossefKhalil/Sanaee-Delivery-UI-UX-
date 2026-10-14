

using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.ProviderCategories.Commands.CreateProviderCategory
{
    public sealed class CreateProviderCategoryCommandHandler(IUnitOfWork unitOfWork) :
        IRequestHandler<CreateProviderCategoryCommand, int>
    {
        public async Task<int> Handle(CreateProviderCategoryCommand request, CancellationToken cts)
        {
            var providerCategory = new ProviderCategory
            {
                YearsOfExperience = request.YearsOfExperience,
                ProviderId = request.ProviderId,
                CategoryId = request.CategoryId
            };

            await unitOfWork.Repository<ProviderCategory>().AddAsync(providerCategory, cts);
            await unitOfWork.CompleteAsync(cts);

            return providerCategory.ProviderCategoryId;
        }
    }
}
