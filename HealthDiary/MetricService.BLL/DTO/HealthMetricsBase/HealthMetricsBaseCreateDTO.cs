namespace MetricService.BLL.DTO.HealthMetricsBase
{
    /// <summary>
    /// Объект данных для регистрации базовых медицинских показателей пользователя
    /// </summary>
    public class HealthMetricsBaseCreateDTO: HealthMetricsBaseBaseDTO
    {        
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>        
        public int UserId { get; set; }
    }
}
