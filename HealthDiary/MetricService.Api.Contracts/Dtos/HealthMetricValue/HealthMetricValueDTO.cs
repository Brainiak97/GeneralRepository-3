namespace MetricService.Api.Contracts.Dtos.HealthMetricValue
{
    /// <summary>
    /// Объект данных значений показателей здоровья пользователя
    /// </summary>
    /// <seealso cref="HealthMetricValueBaseDTO" />
    public record HealthMetricValueDTO : HealthMetricValueBaseDTO
    {
        /// <summary>
        /// Идентификатор данных значения показателя здоровья пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>          
        public int UserId { get; set; }
    }
}
