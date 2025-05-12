using System.Linq.Expressions;

namespace ATMApp.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        void SetDbContext(DatabaseContext context);
        ValueTask<T?> FindByIdAsync(Guid id, CancellationToken token = default);
        Task<List<T>> GetAllAsync(CancellationToken token = default);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);
        Task<int> AddAsync(T entity, CancellationToken token = default);
        Task<int> AddRangeAsync(IEnumerable<T> entities, CancellationToken token = default);
        Task<int> RemoveAsync(T entity, CancellationToken token = default);
        Task<int> RemoveRangeAsync(IEnumerable<T> entities, CancellationToken token = default);
    }
}
