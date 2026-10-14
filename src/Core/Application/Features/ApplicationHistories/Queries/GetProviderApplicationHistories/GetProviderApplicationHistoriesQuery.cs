

using Core.Application.Features.ApplicationHistories.Dtos;
using MediatR;

namespace Core.Application.Features.ApplicationHistories.Queries.GetProviderApplicationHistories
{
    public sealed record GetProviderApplicationHistoriesQuery(int ProviderID) : IRequest<IReadOnlyList<GetApplicationHistoriesDto>>;
    
}
