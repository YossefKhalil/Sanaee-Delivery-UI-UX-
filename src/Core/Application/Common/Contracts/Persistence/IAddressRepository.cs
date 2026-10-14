
using Core.Domain.Entities;

namespace Core.Application.Common.Contracts.Persistence
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        public Task<IReadOnlyList<Address>> GetUserAddressesAsync(int userId, CancellationToken cts);
        public Task<Address?> GetDefaultAddressAsync(int userId, CancellationToken cts);
    }
}
