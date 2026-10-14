

using MediatR;

namespace Core.Application.Features.Offeres.Commands.RejectOffer
{
    public sealed record RejectOfferCommand(int OfferId) : IRequest;
}
