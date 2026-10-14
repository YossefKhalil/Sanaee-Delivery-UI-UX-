using Core.Application.Features.Offeres.Dtos;
using MediatR;

namespace Core.Application.Features.Offeres.Queries.GetAllOffersForOrder
{
    public sealed record GetAllOffersForOrderQuery(int OrderId) : IRequest<IReadOnlyList<GetAllOffersForOrderDto>>;
}
