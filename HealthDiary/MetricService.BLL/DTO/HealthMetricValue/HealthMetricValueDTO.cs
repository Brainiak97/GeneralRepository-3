namespace MetricService.BLL.DTO.HealthMetric
{
    /// <summary>
    /// Объект данных значений показателей здоровья пользователя
    /// </summary>
    /// <seealso cref="HealthMetricValueBaseDTO" />
    public class HealthMetricValueDTO : HealthMetricValueBaseDTO
    {
        /// <summary>
        /// Идентификатор данных значения показателя здоровья пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>          
        public int UserId { get; set; }

        /// <summary>
        /// Показатель здоровья
        /// </summary>         
        public required HealthMetricDTO HealthMetric { get; set; }
    }
}
