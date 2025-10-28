namespace MetricService.Api.Contracts.Dtos.HealthMetricValue
{
    /// <summary>
    /// Объект данных для изменения значений показателей здоровья пользователя
    /// </summary>
    /// <seealso cref="HealthMetricValueBaseDTO" />
    public record HealthMetricValueUpdateDTO : HealthMetricValueBaseDTO
    {
        /// <summary>
        /// Идентификатор данных значения показателя здоровья пользователя
        /// </summary>
        public int Id { get; set; }
    }
}
