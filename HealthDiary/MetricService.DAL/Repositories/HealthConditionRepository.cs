using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о самочувствии(состоянии здоровья) пользователя
    /// </summary>
    /// <seealso cref="BaseRepository&lt;HealthCondition&gt;" />
    /// <seealso cref="IHealthConditionRepository" />
    public class HealthConditionRepository(MetricServiceDbContext metricServiceDb) : BaseRepository<HealthCondition>(metricServiceDb), IHealthConditionRepository
    {
        /// <inheritdoc/>
        public override async Task<HealthCondition?> GetByIdAsync(int id)
        {
            return await _contextDb.HealthConditions
                .Include(h => h.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == id);                
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<HealthCondition>> GetAllAsync()
        {
            return await _contextDb.HealthConditions
                .Include(h => h.User)
                .ToListAsync();
        }
    }
}
