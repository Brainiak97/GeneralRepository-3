namespace MetricService.BLL.DTO.AccessToMetrics
{
    /// <summary>
    /// Объект для регистрации доступа к личным метрикам пользователя
    /// </summary>
    /// <seealso cref="AccessToMetricsBaseDTO" />
    public class AccessToMetricsCreateDTO: AccessToMetricsBaseDTO
    {
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
