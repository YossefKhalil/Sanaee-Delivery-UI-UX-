

using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.ProviderCategories.Commands.ChangeYearsOfExperience
{
    public sealed class ChangeYearsOfExperienceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangeYearsOfExperienceCommand>
    {
        public async Task Handle(ChangeYearsOfExperienceCommand request, CancellationToken cts)
        {
            var providerCategory = 
                await unitOfWork.Repository<ProviderCategory>().GetByIdAsync(request.ProviderCategoryId, cts);

            if (providerCategory is null)
                throw new NotFoundException(nameof(ProviderCategory), request.ProviderCategoryId);

            providerCategory.YearsOfExperience = request.YearsOfExperience;

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
