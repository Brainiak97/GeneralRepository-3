using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    public abstract class ReadBaseRepository<T> : IReadRepository<T> where T : BaseModel
    {
        protected readonly MetricServiceDbContext _contextDb = null!;

        protected ReadBaseRepository(MetricServiceDbContext metricServiceDb)
        {
            _contextDb = metricServiceDb;
        }


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
