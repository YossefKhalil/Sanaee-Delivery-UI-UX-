using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Common.Contracts
{
    public interface IAppDbContext
    {
        DbSet<Message> Messages { get; }
        // أضف أي DbSet تحتاج الوصول إليه في الـ Handlers

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
