

using Core.Application.Features.Orderes.Dtos;
using MediatR;

namespace Core.Application.Features.Orderes.Queries.GetAllOrdersByCustomer
{
    public sealed record GetAllOrdersByCustomerQuery(int CustomerId) : IRequest<IReadOnlyList<GetAllOrdersForCustomerDto>>;
}
