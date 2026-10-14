
using core.Application.Common.Contracts.Persistence;

namespace Core.Application.Common.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository AddressRepository { get; }
        IUserPhoneRepository UserPhoneRepository { get; }
        IWalletRepository WalletRepository { get; }
        IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> CompleteAsync(CancellationToken cts);
    }
}
