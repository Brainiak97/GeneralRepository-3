using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о тренировке пользователя
    /// </summary>
    /// <seealso cref="BaseRepository&lt;Workout&gt;" />
    /// <seealso cref="IWorkoutRepository" />
    public class WorkoutRepository : BaseRepository<Workout>, IWorkoutRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория<see cref="WorkoutRepository"/> class.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public WorkoutRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }
        /// <inheritdoc/>
        public override async Task<bool> UpdateAsync(Workout item)
        {
            Workout? workout = await GetByIdAsync(item.Id);
            if (workout != null)
            {
                workout.StartTime = item.StartTime;
                workout.EndTime = item.EndTime;
                workout.Description = item.Description;
                workout.PhysicalActivityId = item.PhysicalActivityId;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }

        /// <inheritdoc/>
        public async override Task<Workout?> GetByIdAsync(int id)
        {
            return await _contextDb.Workouts
                .Include(w => w.User)
                .Include(w=>w.PhysicalActivity)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        /// <inheritdoc/>
        public async override Task<IEnumerable<Workout>> GetAllAsync()
        {
            return await _contextDb.Workouts
                .Include(w => w.User)
                .Include(w => w.PhysicalActivity)
                .ToListAsync();
        }
    }
}
