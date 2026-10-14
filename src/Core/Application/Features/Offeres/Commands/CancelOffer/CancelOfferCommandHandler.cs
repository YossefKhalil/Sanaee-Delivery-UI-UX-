

using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Offeres.Commands.CancelOffer
{
    public sealed class CancelOfferCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CancelOfferCommand>
    {
        public async Task Handle(CancelOfferCommand request, CancellationToken cts)
        {
            var offer =
                await unitOfWork.Repository<Offer>().GetByIdAsync(request.OfferId, cts);

            if (offer is null)
                throw new NotFoundException(nameof(Offer), request.OfferId);

            offer.Cancel();

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
