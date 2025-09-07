using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о о значениях медицинских показателей пользователя
    /// </summary>
    /// <seealso cref="BaseRepository&lt;HealthMetricValue&gt;" />
    /// <seealso cref="IHealthMetricValueRepository " />
    public class HealthMetricValueRepository : BaseRepository<HealthMetricValue>, IHealthMetricValueRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория<see cref="HealthMetricValueRepository"/> class.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public HealthMetricValueRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        /// <inheritdoc/>
        public override async Task<bool> UpdateAsync(HealthMetricValue item)
        {
            HealthMetricValue? healthMetricValue = await GetByIdAsync(item.Id);
            if (healthMetricValue != null)
            {
                healthMetricValue.Value = item.Value;
                healthMetricValue.RecordedAt = item.RecordedAt;
                healthMetricValue.Comment = item.Comment;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }

        /// <inheritdoc/>
        public async override Task<HealthMetricValue?> GetByIdAsync(int id)
        {
            return await _contextDb.HealthMetricsValue
                .Include(h => h.User)
                .Include(h => h.HealthMetric)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        /// <inheritdoc/>
        public async override Task<IEnumerable<HealthMetricValue>> GetAllAsync()
        {
            return await _contextDb.HealthMetricsValue
                .Include(h => h.User)
                .Include(h => h.HealthMetric)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<HealthMetricValue>> GetListHealthMetricValueByHealthMetricIdAsync(int healthMetricId)
        {
            return await _contextDb.HealthMetricsValue
                .Where(h => h.HealthMetricId == healthMetricId)
               .Include(h => h.User)
               .Include(h => h.HealthMetric)
               .ToListAsync();
        }
    }
}
