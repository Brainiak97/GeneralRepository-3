namespace MetricService.Api.Contracts.Dtos.HealthMetricValue
{
    /// <summary>
    /// Объект данных для регистрации значений показателей здоровья пользователя
    /// </summary>
    /// <seealso cref="HealthMetricValueBaseDTO" />
    public record HealthMetricValueCreateDTO : HealthMetricValueBaseDTO
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>          
        public int UserId { get; init; }
    }
}
