using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UserPhoneRepository : BaseRepository<UserPhone>, IUserPhoneRepository
    {
        public UserPhoneRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> IsDefinedAsync(string PhoneNumber, int UserId, CancellationToken cts)
        {
            return await _context.Set<UserPhone>()
                                 .AsNoTracking()
                                 .AnyAsync(p => p.PhoneNumber == PhoneNumber
                                             && p.UserId == UserId, cts);
        }

        public async Task<IReadOnlyList<UserPhone>> GetAllByUserIdAsync(int UserId, CancellationToken cts)
        {
            return await _context.Set<UserPhone>()
                                 .AsNoTracking()
                                 .Where(p => p.UserId == UserId)
                                 .ToListAsync(cts);
        }

        public async Task<UserPhone?> GetDefaultAsync(int UserId, CancellationToken cts)
        {
            return await _context.Set<UserPhone>()
                                 .FirstOrDefaultAsync(p => p.UserId == UserId && p.IsDefault == true, cts);
                                 
        }
    }
}
