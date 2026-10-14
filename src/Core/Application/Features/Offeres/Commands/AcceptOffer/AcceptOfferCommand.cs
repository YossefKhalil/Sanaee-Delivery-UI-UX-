

using MediatR;

namespace Core.Application.Features.Offeres.Commands.AcceptOffer
{
    public sealed record AcceptOfferCommand(int OfferId) : IRequest;
}
