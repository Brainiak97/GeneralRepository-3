namespace StateService.Api.Contracts.Dtos
{
    public class TempHealthMetricDto
    {
        public DateTime RecordedAt { get; set; }
        public float Value { get; set; }
        public string? Comment { get; set; }
        public TempHealthMetricInfoDto? HealthMetric { get; set; }
    }
}
