using Shared.Common.Exceptions;
using StateService.BLL.Interfaces;
using StateService.DAL.Interfaces;
using StateService.Domain.Models;

namespace StateService.BLL.Services
{
    public class StateService(
        IMetricDataProvider metricDataProvider) : IStateService
    {
        private readonly IMetricDataProvider _metricDataProvider = metricDataProvider;

        public async Task<UserHealthReport> GetDailySummaryAsync(int userId)
        {
            var today = DateTime.Today;
            var reports = await GetPeriodSummaryAsync(userId, today, today);
            
            return reports.FirstOrDefault() ?? throw new EntryNotFoundException("Не удалось получить данные.");
        }

        public async Task<IEnumerable<UserHealthReport>> GetPeriodSummaryAsync(int userId, DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException($"Дата начала периода ({startDate}) должна быть раньше даты окончания периода ({endDate}).");

            var metricsTask = _metricDataProvider.GetHealthMetricsBaseDataAsync(userId, startDate, endDate);
            var workoutsTask = _metricDataProvider.GetWorkoutDataAsync(userId, startDate, endDate);
            var sleepTask = _metricDataProvider.GetSleepDataAsync(userId, startDate, endDate);

            await Task.WhenAll(metricsTask, workoutsTask, sleepTask);

            var metricsList = metricsTask.Result ?? [];
            var workoutsList = workoutsTask.Result ?? [];
            var sleepList = sleepTask.Result ?? [];

            var dateRange = Enumerable
                .Range(0, 1 + endDate.Subtract(startDate).Days)
                .Select(offset => startDate.AddDays(offset))
                .ToList();

            var reports = new List<UserHealthReport>();

            foreach (var date in dateRange)
            {
                var dateAsDate = date.Date;

                var dailyMetrics = metricsList
                    .Where(m => m.MetricDate.Date == dateAsDate)
                    .ToList();

                var dailyWorkouts = workoutsList
                    .Where(w => w.StartTime.Date == dateAsDate)
                    .ToList();

                var dailySleeps = sleepList
                    .Where(s => s.StartSleep.Date == dateAsDate || s.EndSleep.Date == dateAsDate)
                    .ToList();

                reports.Add(new UserHealthReport
                {
                    Date = DateOnly.FromDateTime(date),
                    HealthMetrics = dailyMetrics,
                    PhysicalActivity = dailyWorkouts,
                    Sleep = dailySleeps,
                    FoodData = null
                });
            }

            return reports;
        }        
    }
}
