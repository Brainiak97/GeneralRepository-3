namespace StateService.Api.ViewModels
{
    public class HealthMetricsViewModel
    {
        public string Period { get; set; }
        public double AvgStepsPerDay { get; set; }
        public double SleepHours { get; set; }
        public int HeartRateAvg { get; set; }
        public double Weight { get; set; }
        public string BloodPressure { get; set; }
    }
}
