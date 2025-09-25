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
        /// <inheritdoc />
        public override async Task<bool> UpdateAsync(HealthCondition item)
        {
            HealthCondition? healthCondition = await GetByIdAsync(item.Id);
            if (healthCondition != null)
            {
                healthCondition.Symptoms = item.Symptoms;
                healthCondition.Notes = item.Notes;
                healthCondition.EmotionalState = item.EmotionalState;
                healthCondition.PhysicalState = item.PhysicalState;
                healthCondition.RecordedAt = item.RecordedAt;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }

        /// <inheritdoc/>
        public override async Task<HealthCondition?> GetByIdAsync(int id)
        {
            return await _contextDb.HealthConditions
                .Include(h => h.User)
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
