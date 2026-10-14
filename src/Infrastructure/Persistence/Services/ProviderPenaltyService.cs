

using Core.Application.Common.Contracts;

namespace Infrastructure.Persistence.Services
{
    public class ProviderPenaltyService : IProviderPenaltyService
    {
        public async Task ApplyPenaltyForCancellationAsync(int ProviderId, int OrderId, int ReasonId)
        {
            await Task.CompletedTask;
        }
    }
}
