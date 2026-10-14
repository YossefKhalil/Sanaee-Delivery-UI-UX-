namespace Core.Application.Common.Contracts
{
    public interface IProviderPenaltyService
    {
        Task ApplyPenaltyForCancellationAsync(int ProviderId, int OrderId, int ReasonId);
    }
}
