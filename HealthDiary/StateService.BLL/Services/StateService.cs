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

            // Для  тестов
            var metricsTask = GetHealthTestData();
            var workoutsTask = GetWorkoutTestData();
            var sleepTask = GetSleepTestData();

            var metricsList = metricsTask ?? [];
            var workoutsList = workoutsTask ?? [];
            var sleepList = sleepTask ?? [];

            startDate = DateTime.Now.AddDays(-6);
            endDate = DateTime.Now;

            //var metricsTask = _metricDataProvider.GetHealthMetricsBaseDataAsync(userId.ToString(), startDate, endDate);
            //var workoutsTask = _metricDataProvider.GetWorkoutDataAsync(userId.ToString(), startDate, endDate);
            //var sleepTask = _metricDataProvider.GetSleepDataAsync(userId.ToString(), startDate, endDate);

            //await Task.WhenAll(metricsTask, workoutsTask, sleepTask);

            //var metricsList = metricsTask.Result ?? [];
            //var workoutsList = workoutsTask.Result ?? [];
            //var sleepList = sleepTask.Result ?? [];

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

        private static List<HealthMetricsDto> GetHealthTestData()
        {
            var random = new Random();
            var healthMetrics = new List<HealthMetricsDto>();
            var startDate = DateTime.Now.AddDays(-6);

            for (int i = 0; i < 7; i++)
            {
                var day = startDate.AddDays(i);

                // --- Замеры здоровья: 1–3 в день ---
                var metricCount = random.Next(1, 4);
                for (int j = 0; j < metricCount; j++)
                {
                    var time = day.AddHours(random.Next(8, 20)).AddMinutes(random.Next(0, 59));
                    healthMetrics.Add(new HealthMetricsDto
                    {
                        MetricDate = time,
                        HeartRate = (short)random.Next(60, 85),
                        BloodPressureSys = (short?)random.Next(110, 130),
                        BloodPressureDia = (short?)random.Next(70, 85),
                        BodyFatPercentage = (float?)Math.Round(random.NextDouble() * 15 + 10, 1), // 10–25%
                        WaterIntake = (short?)random.Next(200, 500)
                    });
                }                
            }

            return healthMetrics;
        }

        private static List<SleepDto> GetSleepTestData()
        {
            var random = new Random();
            var sleeps = new List<SleepDto>();
            var startDate = DateTime.Now.AddDays(-6);

            for (int i = 0; i < 7; i++)
            {
                var day = startDate.AddDays(i);

                // --- Сон: 1 сессия в день ---
                var sleepStart = day.AddDays(1).AddHours(-5); // Вчера 23:00 → сегодня 6:00
                var sleepDurationHours = random.Next(6, 9);
                sleeps.Add(new SleepDto
                {
                    StartSleep = sleepStart,
                    EndSleep = sleepStart.AddHours(sleepDurationHours),
                    QualityRating = (short)random.Next(3, 6), // 3–5
                    Comment = random.Next(10) < 3 ? "Чувствовал усталость" : null,
                    SleepDuration = TimeSpan.FromHours(sleepDurationHours)
                });                
            }

            return sleeps;
        }

        private static List<WorkoutDto> GetWorkoutTestData()
        {
            var random = new Random();
            var workouts = new List<WorkoutDto>();
            var startDate = DateTime.Now.AddDays(-6);

            for (int i = 0; i < 7; i++)
            {
                var day = startDate.AddDays(i);

                // --- Тренировки: 0–2 в день ---
                var workoutCount = random.Next(0, 3);
                for (int j = 0; j < workoutCount; j++)
                {
                    var startHour = random.Next(17, 21);
                    var duration = TimeSpan.FromMinutes(random.Next(30, 90));
                    var startTime = day.AddHours(startHour).AddMinutes(random.Next(0, 59));
                    var endTime = startTime + duration;

                    workouts.Add(new WorkoutDto
                    {
                        PhysicalActivityId = random.Next(1, 5), // Условные типы: бег, йога и т.д.
                        StartTime = startTime,
                        EndTime = endTime,
                        Description = random.Next(2) == 0 ? "Интервальная тренировка" : "Кардио",
                        CaloriesBurned = (float)(duration.TotalMinutes * (random.Next(8, 12) / 60.0))
                    });
                }
            }

            return workouts;
        }
    }
}
