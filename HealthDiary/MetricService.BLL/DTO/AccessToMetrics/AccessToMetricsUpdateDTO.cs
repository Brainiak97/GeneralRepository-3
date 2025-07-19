namespace MetricService.BLL.DTO.AccessToMetrics
{
    /// <summary>
    /// Объект для изменения доступа к личным метрикам пользователя
    /// </summary>
    /// <seealso cref="AccessToMetricsBaseDTO" />
    public class AccessToMetricsUpdateDTO: AccessToMetricsBaseDTO
    {
        /// <summary>
        /// Идентификатор данных о доступе к личным метрикам пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому предоставлен доступ к метрикам пользователя
        /// </summary>        
        public int GrantedUserId { get; set; }
    }
}
