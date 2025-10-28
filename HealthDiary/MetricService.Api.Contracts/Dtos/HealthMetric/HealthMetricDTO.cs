namespace MetricService.Api.Contracts.Dtos.HealthMetric
{
    /// <summary>
    /// Объект данных показателей здоровья пользователя
    /// </summary>
    public record HealthMetricDTO : HealthMetricBaseDTO
    {
        /// <summary>
        /// Идентификатор данных показателей здоровья пользователя
        /// </summary>    
        public int Id { get; set; }
    }
}
