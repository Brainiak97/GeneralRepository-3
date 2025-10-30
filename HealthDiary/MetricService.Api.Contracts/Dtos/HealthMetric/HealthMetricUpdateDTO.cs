namespace MetricService.Api.Contracts.Dtos.HealthMetric
{
    /// <summary>
    /// Объект данных для изменения показателей здоровья пользователя
    /// </summary>
    public record HealthMetricUpdateDTO : HealthMetricBaseDTO
    {
        /// <summary>
        /// Идентификатор данных показателей здоровья пользователя
        /// </summary>    
        public int Id { get; init; }
    }
}
