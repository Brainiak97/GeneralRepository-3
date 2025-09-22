using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о медицинских показателях пользователя
    /// </summary>
    /// <seealso cref="BaseRepository&lt;HealthMetric&gt;" />
    /// <seealso cref="IHealthMetricRepository" />
    public class HealthMetricRepository : BaseRepository<HealthMetric>, IHealthMetricRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория<see cref="HealthMetricRepository"/> class.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public HealthMetricRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        /// <inheritdoc/>
        public override async Task<bool> UpdateAsync(HealthMetric item)
        {
            HealthMetric? healthMetric = await GetByIdAsync(item.Id);
            if (healthMetric != null)
            {
                healthMetric.Name = item.Name;
                healthMetric.Description = item.Description;
                healthMetric.Unit = item.Unit;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }
    }
}
