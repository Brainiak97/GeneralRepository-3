using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о базовых медицинских показателях пользователя
    /// </summary>
    /// <seealso cref="BaseRepository&lt;HealthMetricsBase&gt;" />
    /// <seealso cref="MetricService.DAL.Interfaces.IHealthMetricsBaseRepository" />
    public class HealthMetricsBaseRepository : BaseRepository<HealthMetricsBase>, IHealthMetricsBaseRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория<see cref="HealthMetricsBaseRepository"/> class.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public HealthMetricsBaseRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        /// <inheritdoc/>
        public override async Task<bool> UpdateAsync(HealthMetricsBase item)
        {
            HealthMetricsBase? healthMetricsBase = await GetByIdAsync(item.Id);
            if (healthMetricsBase != null)
            {
                healthMetricsBase.BloodPressureSys = item.BloodPressureSys;
                healthMetricsBase.BloodPressureDia = item.BloodPressureDia;
                healthMetricsBase.BodyFatPercentage = item.BodyFatPercentage;
                healthMetricsBase.HeartRate = item.HeartRate;
                healthMetricsBase.MetricDate = item.MetricDate;
                healthMetricsBase.WaterIntake = item.WaterIntake;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }

        /// <inheritdoc/>
        public async override Task<HealthMetricsBase?> GetByIdAsync(int id)
        {
            return await _contextDb.HealthMetricsBase
                .Include(h => h.User)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        /// <inheritdoc/>
        public async override Task<IEnumerable<HealthMetricsBase>> GetAllAsync()
        {
            return await _contextDb.HealthMetricsBase
                .Include(h => h.User)
                .ToListAsync();
        }
    }
}
