

using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Offeres.Commands.CreateOffer
{
    public sealed class CreateOfferCommandHandler(IUnitOfWork unitOfWork) :
        IRequestHandler<CreateOfferCommand, int>
    {
        public async Task<int> Handle(CreateOfferCommand request, CancellationToken cts)
        {
            var order = await unitOfWork.Repository<Order>().GetByIdAsync(request.orderId, cts);

            if (order is null)
                throw new NotFoundException(nameof(Order), request.orderId);


            var offer = Offer.Create
                (
                    order,
                    request.providerId,
                    request.laborCost,
                    request.materialCost,
                    request.notes,
                    request.arrivalTime
                );

            await unitOfWork.Repository<Offer>().AddAsync(offer, cts);
            await unitOfWork.CompleteAsync(cts);

            return offer.Id;
        }
    }
}
