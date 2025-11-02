using MetricService.Api.Contracts.Dtos.HealthMetric;

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
        public int Id { get; init; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>          
        public int UserId { get; init; }

        /// <summary>
        /// Показатель здоровья
        /// </summary>         
        public required HealthMetricDTO HealthMetric { get; set; }
    }
}
