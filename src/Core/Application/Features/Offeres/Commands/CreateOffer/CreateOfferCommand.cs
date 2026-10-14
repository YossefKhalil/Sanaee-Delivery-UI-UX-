

using MediatR;

namespace Core.Application.Features.Offeres.Commands.CreateOffer
{
    public sealed record CreateOfferCommand
        (
            int orderId,
            string? notes,
            int providerId,
            decimal laborCost,
            DateTime arrivalTime,
            decimal? materialCost

        ) : IRequest<int>;
}
