using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    public class HealthMetricsBaseRepository : BaseRepository<HealthMetricsBase>, IHealthMetricsBaseRepository
    {
        public HealthMetricsBaseRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }
        public override async Task<bool> CreateAsync(HealthMetricsBase item)
        {           
            _contextDb.Add(item);
            return await _contextDb.SaveChangesAsync() == 1;            
        }

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

        public async override Task<HealthMetricsBase?> GetByIdAsync(int id)
        {
            return await _contextDb.HealthMetricsBase.Include(h => h.User).FirstOrDefaultAsync(h => h.Id == id);
        }

        public async override Task<IEnumerable<HealthMetricsBase>> GetAllAsync()
        {
            return await _contextDb.HealthMetricsBase.Include(h => h.User)
                 .ToListAsync();
        }
    }
}
