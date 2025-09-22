namespace MetricService.BLL.DTO.HealthMetric
{
    /// <summary>
    /// Объект данных для изменения значений показателей здоровья пользователя
    /// </summary>
    /// <seealso cref="HealthMetricValueBaseDTO" />
    public class HealthMetricValueUpdateDTO : HealthMetricValueBaseDTO
    {
        /// <summary>
        /// Идентификатор данных значения показателя здоровья пользователя
        /// </summary>
        public int Id { get; set; }
    }
}
