
using core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Contracts.Persistence;
using Persistence.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();


        private IAddressRepository? _addressRepository;
        private IUserPhoneRepository? _userPhoneRepository;
        private IWalletRepository? _walletRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IAddressRepository AddressRepository 
            => _addressRepository ??= new AddressRepository(_context);

        public IUserPhoneRepository UserPhoneRepository 
            => _userPhoneRepository ??= new UserPhoneRepository(_context);

        public IWalletRepository WalletRepository
            => _walletRepository ??= new WalletRepository(_context);

        public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class
        {

            var entityType = typeof(TEntity);

            if (!_repositories.TryGetValue(entityType, out var repository))
            {
                var repositoryType = typeof(BaseRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositoryType.MakeGenericType(entityType), _context);

                if (repositoryInstance is null)
                    throw new InvalidOperationException($"Could not create repository instance for {entityType.Name}");

                repository = repositoryInstance;
                _repositories.Add(entityType, repository);
            }

            return (IBaseRepository<TEntity>)repository;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> CompleteAsync(CancellationToken cts)
        {
            return await _context.SaveChangesAsync(cts);
        }
    }
}
