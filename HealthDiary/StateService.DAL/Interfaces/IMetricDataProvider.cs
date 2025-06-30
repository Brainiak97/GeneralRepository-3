using MetricService.Domain.Models;

namespace StateService.DAL.Interfaces
{
    public interface IMetricDataProvider
    {
        Task<HealthMetricsBase> GetHealthMetricsBaseDataAsync(string userId);
        Task<PhysicalActivity> GetPhysicalActivityDataAsync(string userId);
        Task<Sleep> GetSleepDataAsync(string userId);
        Task<Workout> GetWorkoutDataAsync(string userId);
    }
}
