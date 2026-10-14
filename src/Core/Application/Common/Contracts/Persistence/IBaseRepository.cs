using System.Linq.Expressions;

namespace Core.Application.Common.Contracts.Persistence
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<T?> GetByIdAsync(int Id, CancellationToken cts);
        public Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cts);
        public Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cts);
        public Task AddAsync(T entity, CancellationToken cts);
        public void Update(T entity);
        public void Delete(T entity);
        public IQueryable<T> GetQueryable();
    }
}
