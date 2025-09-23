using StateService.BLL.Interfaces;
using StateService.DAL.Interfaces;
using StateService.Domain.Dto;
using StateService.Domain.Models;

namespace StateService.BLL.Services
{
    public class StateService(
        IUserDataProvider userDataProvider,
        IMetricDataProvider metricDataProvider,
        IFoodDataProvider foodDataProvider) : IStateService
    {
        private readonly IUserDataProvider _userDataProvider = userDataProvider;
        private readonly IMetricDataProvider _metricDataProvider = metricDataProvider;
        private readonly IFoodDataProvider _foodDataProvider = foodDataProvider;

        public async Task<UserHealthReport> GetDailySummaryAsync(int userId)
        {
            var today = DateTime.Today;
            var reports = await GetPeriodSummaryAsync(userId, today, today);
            
            return reports.FirstOrDefault() ?? throw new ArgumentNullException(nameof(userId));
        }

        public async Task<IEnumerable<UserHealthReport>> GetPeriodSummaryAsync(int userId, DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("Дата начала периода должна быть раньше даты окончания периода.");

            var metricsTask = _metricDataProvider.GetHealthMetricsBaseDataAsync(userId.ToString(), startDate, endDate);
            var workoutsTask = _metricDataProvider.GetWorkoutDataAsync(userId.ToString(), startDate, endDate);
            var sleepTask = _metricDataProvider.GetSleepDataAsync(userId.ToString(), startDate, endDate);

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
