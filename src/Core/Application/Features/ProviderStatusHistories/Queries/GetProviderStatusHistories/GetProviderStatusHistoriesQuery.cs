

using Core.Application.Features.ProviderStatusHistories.Dto;
using MediatR;

namespace Core.Application.Features.ProviderStatusHistories.Queries.GetProviderStatusHistories
{
    /// <summary>
    /// Get all Status Histories for a provider to show it to Admin
    /// </summary>
    /// <param name="ProviderId"></param>
    public sealed record GetProviderStatusHistoriesQuery(int ProviderId) : IRequest<IReadOnlyList<GetProviderStatusHistoriesDto>>;
    
}
