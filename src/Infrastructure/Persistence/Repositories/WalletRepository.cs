
using core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> IsDefinedAsync(int UserId, CancellationToken cts)
        {
            return await _context.Set<Wallet>()
                .AsNoTracking()
                .AnyAsync(w => w.UserId == UserId);
        }
    }
}
