namespace MetricService.BLL.DTO.HealthMetric
{
    /// <summary>
    /// Объект данных для изменения показателей здоровья пользователя
    /// </summary>
    public class HealthMetricUpdateDTO : HealthMetricBaseDTO
    {
        /// <summary>
        /// Идентификатор данных показателей здоровья пользователя
        /// </summary>    
        public int Id { get; set; }
    }
}
