
using Core.Application.Common.Models;
using Core.Application.Features.Customers.Dtos;
using MediatR;

namespace Core.Application.Features.Customers.Queries.GetAllCustomersForAdmin
{
    public sealed record GetAllCustomersForAdminQuery
        ( 
            int PageNumber = 1,
            int PageSize = 10,
            string? SearchTerm = null

        ) : IRequest<PagedResult<UserDto>>;

}
