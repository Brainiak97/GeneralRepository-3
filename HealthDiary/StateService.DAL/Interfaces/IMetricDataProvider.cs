using StateService.Domain.Dto;

namespace StateService.DAL.Interfaces
{
    public interface IMetricDataProvider
    {
        // Получить базовые метрики за период
        Task<List<HealthMetricsDto>> GetHealthMetricsBaseDataAsync(string userId, DateTime startDate, DateTime endDate);

        // Получить тренировки за период
        Task<List<WorkoutDto>> GetWorkoutDataAsync(string userId, DateTime startDate, DateTime endDate);

        // Получить сон за период
        Task<List<SleepDto>> GetSleepDataAsync(string userId, DateTime startDate, DateTime endDate);
    }
}
