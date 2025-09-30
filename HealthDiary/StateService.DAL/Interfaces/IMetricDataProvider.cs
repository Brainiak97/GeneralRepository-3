using StateService.Domain.Dto;

namespace StateService.DAL.Interfaces
{
    public interface IMetricDataProvider
    {
        // Получить базовые метрики за период
        Task<List<HealthMetricsDto>> GetHealthMetricsBaseDataAsync(int userId, DateTime startDate, DateTime endDate);

        // Получить тренировки за период
        Task<List<WorkoutDto>> GetWorkoutDataAsync(int userId, DateTime startDate, DateTime endDate);

        // Получить сон за период
        Task<List<SleepDto>> GetSleepDataAsync(int userId, DateTime startDate, DateTime endDate);
    }
}
