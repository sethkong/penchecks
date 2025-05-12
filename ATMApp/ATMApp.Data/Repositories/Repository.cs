using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ATMApp.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DatabaseContext? _context;

        public void SetDbContext(DatabaseContext? context)
        {
            _context = context;
        }

        public Task<int> AddAsync(T entity, CancellationToken token = default)
        {
            if (_context == null)
                throw new ArgumentNullException("DbContext is null");
            _context.Set<T>().Add(entity);
            return _context.SaveChangesAsync(token);
        }

        public Task<int> AddRangeAsync(IEnumerable<T> entities, CancellationToken token = default)
        {
            if (_context == null)
                throw new ArgumentNullException("DbContext is null");
            _context.Set<T>().AddRange(entities);
            return _context.SaveChangesAsync(token);
        }

        public Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
        {
            if (_context == null)
                throw new ArgumentNullException("DbContext is null");
            return _context.Set<T>().Where(predicate).ToListAsync(token);
        }

        public Task<List<T>> GetAllAsync(CancellationToken token = default)
        {
            if (_context == null)
                throw new ArgumentNullException("DbContext is null");
            return _context.Set<T>().ToListAsync();
        }

        public ValueTask<T?> FindByIdAsync(Guid id, CancellationToken token = default)
        {
            if (_context == null)
                throw new ArgumentNullException("DbContext is null");
            return _context.Set<T>().FindAsync(id, token);
        }

        public Task<int> RemoveAsync(T entity, CancellationToken token = default)
        {
            if (_context == null)
                throw new ArgumentNullException("DbContext is null");
            _context.Set<T>().Remove(entity);
            return _context.SaveChangesAsync();
        }

        public Task<int> RemoveRangeAsync(IEnumerable<T> entities, CancellationToken token = default)
        {
            if (_context == null)
                throw new ArgumentNullException("DbContext is null");
            return _context.SaveChangesAsync();
        }
    }
}
