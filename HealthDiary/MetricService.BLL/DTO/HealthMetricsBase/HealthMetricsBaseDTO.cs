namespace MetricService.BLL.DTO.HealthMetricsBase
{
    public  class HealthMetricsBaseDTO: HealthMetricsBaseBaseDTO
    {
        /// <summary>
        /// идентификатор       
        /// </summary>    
        public int Id { get; set; }
        /// <summary>
        /// идентификатор пользователя
        /// </summary>        
        public int UserId { get; set; }
    }
}
