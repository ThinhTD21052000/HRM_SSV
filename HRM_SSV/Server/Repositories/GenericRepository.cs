using Microsoft.EntityFrameworkCore;
using Server.DBContext;
using System.Linq.Expressions;

namespace Server.Repositories
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(Expression<Func<T, bool>> filter = null);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly HRM_DbContext _db;
        public GenericRepository(HRM_DbContext db)
        {
            _db = db;
        }

        public async Task Add(T entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _db.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter = null)
        {
            return await _db.Set<T>().FirstOrDefaultAsync(filter);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task Update(T entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
