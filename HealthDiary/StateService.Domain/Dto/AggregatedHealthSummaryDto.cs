namespace StateService.Domain.Dto
{
    public class AggregatedHealthSummaryDto
    {
        public string Period { get; set; } = string.Empty;

        // Средние показатели
        public double AvgHeartRate { get; set; }
        public double? AvgBloodPressureSys { get; set; }
        public double? AvgBloodPressureDia { get; set; }
        public double? AvgBodyFatPercentage { get; set; }

        // Сон
        public double AvgSleepDurationHours { get; set; }
        public double AvgSleepQuality { get; set; }

        // Активность
        public double TotalCaloriesBurned { get; set; }
        public int WorkoutCount { get; set; }

        // Вода
        public double AvgDailyWaterIntake { get; set; }
    }
}
