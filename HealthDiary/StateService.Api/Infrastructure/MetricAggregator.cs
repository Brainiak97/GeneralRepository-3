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

            // === Агрегация всех метрик по имени ===
            var allMetrics = reportsList
                .Where(r => r.HealthMetrics != null)
                .SelectMany(r => r.HealthMetrics!)
                .ToList();

            var aggregatedMetrics = allMetrics
                .Where(m => m.Value.HasValue)
                .GroupBy(m => m.MetricName) // Группируем по имени метрики
                .ToDictionary(
                    g => g.Key,
                    g => (double?)g.Average(m => m.Value!.Value)
                );

            // === Сон ===
            var sleepEntries = reportsList
                .Where(r => r.Sleep != null)
                .SelectMany(r => r.Sleep!)
                .ToList();

            // === Тренировки ===
            var workouts = reportsList
                .Where(r => r.PhysicalActivity != null)
                .SelectMany(r => r.PhysicalActivity!)
                .ToList();

            return new AggregatedHealthSummaryDto
            {
                Period = period,
                AggregatedMetrics = aggregatedMetrics,

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
