

using Core.Application.Common.Models;
using MediatR;
using Core.Application.Features.Provideres.Dtos;

namespace Core.Application.Features.Provideres.Queries.GetAllProviders
{
    public sealed record GetAllProvidersQuery(

            int PageNumber = 1,
            int PageSize = 10,
            string? SearchTerm = null

        ) : IRequest<PagedResult<ProviderDto>>;

}
