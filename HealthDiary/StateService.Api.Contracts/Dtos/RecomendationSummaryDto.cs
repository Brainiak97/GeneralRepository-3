namespace StateService.Api.Contracts.Dtos
{
    public class RecomendationSummaryDto
    {
        public required List<HealthMetricsDto> HealthMetrics { get; set; }

        public double AvgSleepDurationHours { get; set; }

        public double AvgSleepQuality { get; set; }

        public int WorkoutCount { get; set; }

        public float TotalCaloriesBurned { get; set; }
    }
}
