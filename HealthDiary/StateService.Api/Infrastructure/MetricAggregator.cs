using Shared.Common.Exceptions;
using StateService.Domain.Dto;
using StateService.Domain.Models;

namespace StateService.Api.Infrastructure
{
    public static class MetricAggregator
    {
        public static AggregatedHealthSummaryDto AggregateHealthData(IEnumerable<UserHealthReport> reports)
        {
            var reportsList = reports.ToList();
            if (reportsList.Count == 0)
                throw new EntryNotFoundException("Нет данных.");

            var startDate = reportsList.Min(r => r.Date);
            var endDate = reportsList.Max(r => r.Date);
            var period = $"{startDate:dd.MM.yyyy} – {endDate:dd.MM.yyyy}";

            // Собираем ВСЕ метрики (без группировки, без усреднения на этом этапе)
            var allMetrics = reportsList
                .Where(r => r.HealthMetrics != null)
                .SelectMany(r => r.HealthMetrics!)
                .Where(m => m.Value.HasValue) // опционально: фильтруем только с данными
                .ToList();

            var sleepEntries = reportsList
                .Where(r => r.Sleep != null)
                .SelectMany(r => r.Sleep!)
                .ToList();

            var workouts = reportsList
                .Where(r => r.PhysicalActivity != null)
                .SelectMany(r => r.PhysicalActivity!)
                .ToList();

            return new AggregatedHealthSummaryDto
            {
                Period = period,
                HealthMetrics = allMetrics, // ← сохраняем исходные объекты

                AvgSleepDurationHours = sleepEntries.Count != 0
                    ? sleepEntries.Average(s => s.SleepDuration.TotalHours)
                    : 0,
                AvgSleepQuality = sleepEntries.Count != 0
                    ? sleepEntries.Average(s => (double)s.QualityRating)
                    : 0,

                WorkoutCount = workouts.Count,
                TotalCaloriesBurned = workouts.Sum(w => w.CaloriesBurned)
            };
        }
    }
}
