using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Address>> GetUserAddressesAsync(int userId, CancellationToken cts)
        {
            return await _context.Set<Address>()
                                 .AsNoTracking()
                                 .Where(a => a.UserId == userId)
                                 .ToListAsync(cts);
        }

        public async Task<Address?> GetDefaultAddressAsync(int userId, CancellationToken cts)
        {
            return await _context.Set<Address>()
                                 .FirstOrDefaultAsync(a => a.UserId == userId && a.IsDefault, cts);
        }
    }
}
