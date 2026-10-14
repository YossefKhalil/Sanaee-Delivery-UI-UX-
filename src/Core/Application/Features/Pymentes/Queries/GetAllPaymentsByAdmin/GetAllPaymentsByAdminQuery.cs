using Core.Application.Common.Models;
using Core.Application.Features.Pymentes.Dtos;
using MediatR;

namespace Core.Application.Features.Pymentes.Queries.GetAllPaymentsByAdmin
{
    public sealed record GetAllPaymentsByAdminQuery
        (
            int PageNumber = 1,
            int PageSize = 10

        ) : IRequest<PagedResult<GetPaymentDto>>;
}
