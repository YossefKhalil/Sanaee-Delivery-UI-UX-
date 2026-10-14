

using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Offeres.Commands.RejectOffer
{
    public sealed class RejectOfferCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RejectOfferCommand>
    {
        public async Task Handle(RejectOfferCommand request, CancellationToken cts)
        {
            var offer =
                await unitOfWork.Repository<Offer>().GetByIdAsync(request.OfferId, cts);

            if (offer is null)
                throw new NotFoundException(nameof(Offer), request.OfferId);

            offer.Reject();

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
