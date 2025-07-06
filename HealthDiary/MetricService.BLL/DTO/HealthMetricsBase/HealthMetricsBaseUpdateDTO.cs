namespace MetricService.BLL.DTO.HealthMetricsBase
{
    /// <summary>
    /// Объект данных для изменения базовых медицинских показателей пользователя
    /// </summary>
    public class HealthMetricsBaseUpdateDTO : HealthMetricsBaseBaseDTO
    {
        /// <summary>
        /// Идентификатор данных базовых медицинских показателей пользователя  
        /// </summary>    
        public int Id { get; set; }
    }
}
