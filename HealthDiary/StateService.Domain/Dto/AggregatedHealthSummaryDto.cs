namespace StateService.Domain.Dto
{
    public class AggregatedHealthSummaryDto
    {
        /// <summary>
        /// Период отчёта (например, "01.04.2025 – 10.04.2025")
        /// </summary>
        public string Period { get; set; } = null!;

        /// <summary>
        /// Усреднённые значения метрик по их имени
        /// Ключ — MetricName, значение — среднее (или null, если данных нет)
        /// </summary>
        public List<HealthMetricsDto> HealthMetrics { get; set; } = new();

        // --- Сон ---
        public double AvgSleepDurationHours { get; set; }
        public double AvgSleepQuality { get; set; }

        // --- Физическая активность ---
        public int WorkoutCount { get; set; }
        public float TotalCaloriesBurned { get; set; }
    }
}
