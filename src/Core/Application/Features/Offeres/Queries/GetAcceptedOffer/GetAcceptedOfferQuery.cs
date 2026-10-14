

using Core.Application.Features.Offeres.Dtos;
using MediatR;

namespace Core.Application.Features.Offeres.Queries.GetAcceptedOffer
{
    public sealed record GetAcceptedOfferQuery(int OrderId) : IRequest<GetOfferDto>;
}
