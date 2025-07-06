namespace MetricService.BLL.DTO.HealthMetricsBase
{
    /// <summary>
    /// Объект данных базовых медицинских показателей пользователя
    /// </summary>
    public  class HealthMetricsBaseDTO: HealthMetricsBaseBaseDTO
    {
        /// <summary>
        /// Идентификатор данных базовых медицинских показателей пользователя     
        /// </summary>    
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>        
        public int UserId { get; set; }
    }
}
