

using Core.Domain.Entities;

namespace Core.Application.Common.Contracts.Persistence
{
    public interface IUserPhoneRepository : IBaseRepository<UserPhone>
    {
        public Task<bool> IsDefinedAsync(string PhoneNumber, int UserId, CancellationToken cts);

        public Task<IReadOnlyList<UserPhone>> GetAllByUserIdAsync(int UserId, CancellationToken cts);
        public Task<UserPhone?> GetDefaultAsync(int UserId, CancellationToken cts);
    }
}
