using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию базового репозитория для работы с данными
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IRepository&lt;T&gt;" />
    public abstract class  BaseRepository<T> : IRepository<T> where T : BaseModel
    {
        /// <summary>
        /// Контекст базы данных MetricService
        /// </summary>
        protected readonly MetricServiceDbContext _contextDb = null!;

        /// <summary>
        /// Cоздать новый объект репозитория<see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        protected BaseRepository(MetricServiceDbContext metricServiceDb)
        {
            _contextDb = metricServiceDb;
        }

        /// <inheritdoc/>
        public virtual string Name => _contextDb.Set<T>().EntityType.ClrType.Name;

        /// <inheritdoc/>
        public virtual async Task<bool> CreateAsync(T item)
        {
            _contextDb.Add(item);
            return await _contextDb.SaveChangesAsync() == 1;
        }

        /// <inheritdoc/>
        public virtual async Task<bool> DeleteAsync(int id)
        {
            T? entity = await _contextDb.Set<T>().FindAsync(id);

            if (entity != null)
            {
                _contextDb.Set<T>().Remove(entity);
            }
            
            return await _contextDb.SaveChangesAsync() == 1;
        }

        /// <inheritdoc/>
        public abstract Task<bool> UpdateAsync(T item);

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _contextDb.Set<T>().ToListAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _contextDb.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
