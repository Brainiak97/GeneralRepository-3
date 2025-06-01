using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    public class WorkoutRepository : WriteBaseRepository<Workout>, IWorkoutRepository
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
                workout.UserId = item.UserId;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }
    }
}
