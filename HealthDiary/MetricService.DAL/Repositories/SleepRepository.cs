using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    public class SleepRepository : WriteBaseRepository<Sleep>, ISleepRepository
    {
        public SleepRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb) { }


        public override async Task<bool> CreateAsync(Sleep item)
        {            
            if (!_contextDb.Sleeps.Any(s =>s.Id == item.Id))
            {
                _contextDb.Add(item);
                return await _contextDb.SaveChangesAsync() == 1;
            }
            return await UpdateAsync(item);
        }
        public override async Task<bool> UpdateAsync(Sleep item)
        {
            Sleep? sleep = await GetByIdAsync(item.Id);
            if (sleep != null)
            {                
                sleep.StartSleep = item.StartSleep;
                sleep.User = item.User;
                sleep.EndSleep = item.EndSleep;
                sleep.QualityRating = item.QualityRating;
                sleep.Comment = item.Comment;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }
        public override async Task<Sleep?> GetByIdAsync(int id)
        {            
            return await _contextDb.Sleeps.Include(s=>s.User).FirstOrDefaultAsync(s => s.Id == id);
        }

        public override async Task<IEnumerable<Sleep>> GetAllAsync()
        {
            return await _contextDb.Sleeps.Include(s=>s.User)
                .ToListAsync();
        }


    }


}
