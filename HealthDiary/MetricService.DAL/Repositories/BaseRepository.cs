using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    public abstract class  BaseRepository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly MetricServiceDbContext _contextDb = null!;
        protected BaseRepository(MetricServiceDbContext metricServiceDb)
        {
            _contextDb = metricServiceDb;
        }

        public virtual string Name => _contextDb.Set<T>().EntityType.ClrType.Name;

        public virtual async Task<bool> CreateAsync(T item)
        {
            _contextDb.Add(item);
            return await _contextDb.SaveChangesAsync() == 1;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            T? entity = await _contextDb.Set<T>().FindAsync(id);

            if (entity != null)
            {
                _contextDb.Set<T>().Remove(entity);
            }
            
            return await _contextDb.SaveChangesAsync() == 1;
        }

        public abstract Task<bool> UpdateAsync(T item);

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _contextDb.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _contextDb.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
