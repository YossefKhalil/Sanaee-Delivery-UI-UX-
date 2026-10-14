
using Core.Application.Features.Offeres.Dtos;
using MediatR;

namespace Core.Application.Features.Offeres.Queries.GetOfferById
{
    public sealed record GetOfferByIdQuery(int OfferId) : IRequest<GetOfferDto>;
}
