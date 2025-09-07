namespace MetricService.BLL.DTO.HealthMetric
{
    /// <summary>
    /// Объект данных для регистрации значений показателей здоровья пользователя
    /// </summary>
    /// <seealso cref="HealthMetricValueBaseDTO" />
    public class HealthMetricValueCreateDTO : HealthMetricValueBaseDTO
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>          
        public int UserId { get; set; }
    }
}
