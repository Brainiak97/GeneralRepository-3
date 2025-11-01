namespace MetricService.Api.Contracts.Dtos.AccessToMetrics
{
    /// <summary>
    /// Объект для регистрации доступа к личным метрикам пользователя
    /// </summary>
    /// <seealso cref="AccessToMetricsBaseDTO" />
    public record AccessToMetricsCreateDTO : AccessToMetricsBaseDTO
    {
        /// <summary>
        /// Идентификатор пользователя, предоставляющий доступ к своим метрикам
        /// </summary>       
        public int ProviderUserId { get; init; }
    }
}
