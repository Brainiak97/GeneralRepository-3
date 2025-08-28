using StateService.Domain.Dto;
using StateService.Domain.Models;

namespace StateService.Api.Infrastructure
{
    public static class MetricAggregator
    {
        public static AggregatedHealthSummaryDto AggregateHealthData(IEnumerable<UserHealthReport> reports)
        {
            if (!reports.Any())
                throw new ArgumentException("Нет данных за указанный период");

            var startDate = reports.Min(r => r.Date);
            var endDate = reports.Max(r => r.Date);
            var period = $"{startDate:dd.MM.yyyy} – {endDate:dd.MM.yyyy}";

            var summary = new AggregatedHealthSummaryDto
            {
                Period = period
            };

            // --- Собираем метрики здоровья ---
            var allMetrics = reports
                .Where(r => r.HealthMetrics != null)
                .SelectMany(r => r.HealthMetrics!)
                .ToList();

            if (allMetrics.Count != 0)
            {
                summary.AvgHeartRate = allMetrics.Count != 0
                    ? allMetrics.Average(m => (double)m.HeartRate)
                    : 0;

                var sysPressures = allMetrics
                    .Where(m => m.BloodPressureSys.HasValue)
                    .Select(m => (double)m.BloodPressureSys!.Value);
                summary.AvgBloodPressureSys = sysPressures.Any() ? sysPressures.Average() : null;

                var diaPressures = allMetrics
                    .Where(m => m.BloodPressureDia.HasValue)
                    .Select(m => (double)m.BloodPressureDia!.Value);
                summary.AvgBloodPressureDia = diaPressures.Any() ? diaPressures.Average() : null;

                var bodyFat = allMetrics
                    .Where(m => m.BodyFatPercentage.HasValue)
                    .Select(m => (double)m.BodyFatPercentage!.Value);
                summary.AvgBodyFatPercentage = bodyFat.Any() ? bodyFat.Average() : null;

                var water = allMetrics
                    .Where(m => m.WaterIntake.HasValue)
                    .Select(m => (double)m.WaterIntake!.Value);
                summary.AvgDailyWaterIntake = water.Any() ? water.Average() : 0;
            }

            // --- Сон ---
            var sleepEntries = reports
                .Where(r => r.Sleep != null)
                .SelectMany(r => r.Sleep!)
                .ToList();

            if (sleepEntries.Count != 0)
            {
                summary.AvgSleepDurationHours = sleepEntries.Average(s => s.SleepDuration.TotalHours);
                summary.AvgSleepQuality = sleepEntries.Average(s => s.QualityRating);
            }

            // --- Физическая активность ---
            var workouts = reports
                .Where(r => r.PhysicalActivity != null)
                .SelectMany(r => r.PhysicalActivity!)
                .ToList();

            if (workouts.Count != 0)
            {
                summary.WorkoutCount = workouts.Count;
                summary.TotalCaloriesBurned = workouts.Sum(w => w.CaloriesBurned);
            }

            return summary;
        }
    }
}
