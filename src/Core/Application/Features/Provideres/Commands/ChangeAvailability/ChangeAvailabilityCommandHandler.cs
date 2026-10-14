

using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Provideres.Commands.ChangeAvailability
{
    public sealed class ChangeAvailabilityCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangeAvailabilityCommand>
    {
        public async Task Handle(ChangeAvailabilityCommand request, CancellationToken cts)
        {
            var provider = await unitOfWork.Repository<Provider>().GetByIdAsync(request.ProviderId, cts);

            if (provider is null)
                throw new NotFoundException(nameof(Provider), request.ProviderId);

            provider.IsAvailable = !provider.IsAvailable;

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
