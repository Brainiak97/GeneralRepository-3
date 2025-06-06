using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    public class WorkoutRepository : BaseRepository<Workout>, IWorkoutRepository
    {
        public WorkoutRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        public override async Task<bool> CreateAsync(Workout item)
        {
            item.Id = 0;
            _contextDb.Add(item);
            return await _contextDb.SaveChangesAsync() == 1;
        }

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

        public async override Task<Workout?> GetByIdAsync(int id)
        {
            return await _contextDb.Workouts.Include(w => w.User).Include(w=>w.PhysicalActivity)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async override Task<IEnumerable<Workout>> GetAllAsync()
        {
            return await _contextDb.Workouts.Include(w => w.User).Include(w => w.PhysicalActivity)
                .ToListAsync();
        }
    }
}
