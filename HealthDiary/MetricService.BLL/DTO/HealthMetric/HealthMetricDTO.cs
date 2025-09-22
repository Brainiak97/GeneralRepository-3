namespace MetricService.BLL.DTO.HealthMetric
{
    /// <summary>
    /// Объект данных показателей здоровья пользователя
    /// </summary>
    public class HealthMetricDTO : HealthMetricBaseDTO
    {
        /// <summary>
        /// Идентификатор данных показателей здоровья пользователя
        /// </summary>    
        public int Id { get; set; }
    }
}
