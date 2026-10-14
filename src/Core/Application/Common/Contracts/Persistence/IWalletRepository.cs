

using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;

namespace core.Application.Common.Contracts.Persistence
{
    public interface IWalletRepository : IBaseRepository<Wallet>
    {
        public Task<bool> IsDefinedAsync(int UserId, CancellationToken cts);
    }
}
