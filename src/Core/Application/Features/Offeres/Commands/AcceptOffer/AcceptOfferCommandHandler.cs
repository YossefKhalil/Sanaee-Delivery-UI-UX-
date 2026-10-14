
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Offeres.Commands.AcceptOffer
{
    public sealed class AcceptOfferCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AcceptOfferCommand>
    {
        public async Task Handle(AcceptOfferCommand request, CancellationToken cts)
        {
            var offer =
                await unitOfWork.Repository<Offer>().GetByIdAsync(request.OfferId, cts);

            if (offer is null)
                throw new NotFoundException(nameof(Offer), request.OfferId);

            offer.Accept();

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
