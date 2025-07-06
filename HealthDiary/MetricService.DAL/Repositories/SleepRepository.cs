using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    ///  Предоставляет реализацию репозитория для работы с данными о сне пользователя
    /// </summary>
    /// <seealso cref="BaseRepository&lt;Sleep&gt;" />
    /// <seealso cref="ISleepRepository" />
    public class SleepRepository : BaseRepository<Sleep>, ISleepRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория<see cref="SleepRepository"/> class.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public SleepRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb) { }

        /// <inheritdoc/>
        public override async Task<bool> UpdateAsync(Sleep item)
        {
            Sleep? sleep = await GetByIdAsync(item.Id); 
            if (sleep != null)
            {
                 sleep.StartSleep = item.StartSleep;
                 sleep.EndSleep = item.EndSleep;
                 sleep.Comment = item.Comment;
                 sleep.QualityRating = item.QualityRating;                
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }

        /// <inheritdoc/>
        public override async Task<Sleep?> GetByIdAsync(int id)
        {
            return await _contextDb.Sleeps
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<Sleep>> GetAllAsync()
        {
            return await _contextDb.Sleeps
                .Include(s => s.User)
                .ToListAsync();
        }               
    }
}
