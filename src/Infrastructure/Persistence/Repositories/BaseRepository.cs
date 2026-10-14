using Core.Application.Common.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity, CancellationToken cts)
        {
            await _context.Set<T>().AddAsync(entity, cts);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cts)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync(cts);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cts)
        {
            return await _context.Set<T>().ToListAsync(cts);
        }

        public async Task<T?> GetByIdAsync(int Id, CancellationToken cts)
        {
            return await _context.Set<T>().FindAsync(Id, cts);
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
