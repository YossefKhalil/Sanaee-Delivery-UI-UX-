

using Core.Application.Features.Customers.Dtos;
using MediatR;

namespace Core.Application.Features.Customers.Queries.GetCustomerById
{
    public sealed record GetCustomerByIdQuery(int UserId) : IRequest<UserDto>;

}
