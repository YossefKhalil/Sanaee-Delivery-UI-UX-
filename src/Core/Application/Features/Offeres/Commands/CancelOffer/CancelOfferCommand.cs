

using MediatR;

namespace Core.Application.Features.Offeres.Commands.CancelOffer
{
    public sealed record CancelOfferCommand(int OfferId) : IRequest;
}
