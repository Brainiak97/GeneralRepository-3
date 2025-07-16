namespace MetricService.BLL.DTO.AccessToMetrics
{
    /// <summary>
    /// Объект данных о доступе к личным метрикам пользователя
    /// </summary>
    /// <seealso cref="AccessToMetricsBaseDTO" />
    public class AccessToMetricsDTO: AccessToMetricsBaseDTO
    {
        /// <summary>
        /// Идентификатор данных о доступе к личным метрикам пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя, предоставляющий доступ к своим метрикам
        /// </summary>       
        public int ProviderUserId { get; set; }               

        /// <summary>
        /// Идентификатор пользователя, которому предоставлен доступ к метрикам пользователя
        /// </summary>        
        public int GrantedUserId { get; set; }        
    }
}
