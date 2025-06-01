using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.DAL.Repositories
{
    public class HealthMetricsBaseRepository : WriteBaseRepository<HealthMetricsBase>, IHealthMetricsBaseRepository
    {
        public HealthMetricsBaseRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }


        public override async Task<bool> CreateAsync(HealthMetricsBase item)
        {
            item.Id = 0;
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
                healthMetricsBase.UserId = item.UserId;
                healthMetricsBase.WaterIntake = item.WaterIntake;
                healthMetricsBase.HeartRate = item.HeartRate;
                healthMetricsBase.MetricDate = item.MetricDate;
                healthMetricsBase.BodyFatPercentage = item.BodyFatPercentage;

            }
            return await _contextDb.SaveChangesAsync() == 1;
        }
    }
}
