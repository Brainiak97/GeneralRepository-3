namespace StateService.Domain.Dto
{
    public class TempHealthMetric
    {
        public DateTime RecordedAt { get; set; }
        public float Value { get; set; }
        public string? Comment { get; set; }
        public TempHealthMetricInfo? HealthMetric { get; set; }
    }
}
