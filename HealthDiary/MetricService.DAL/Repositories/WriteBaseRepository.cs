using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.DAL.Repositories
{
    public abstract class  WriteBaseRepository<T> : ReadBaseRepository<T>, IWriteRepository<T> where T : BaseModel
    {
        protected WriteBaseRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        public abstract Task<bool> CreateAsync(T item);

        public virtual async Task<bool> DeleteAsync(int id)
        {
            T? entity = await _contextDb.Set<T>().FindAsync(id);

            if (entity != null)

                _contextDb.Set<T>().Remove(entity);

            return await _contextDb.SaveChangesAsync() == 1;
        }

        public abstract Task<bool> UpdateAsync(T item);
    }
}
