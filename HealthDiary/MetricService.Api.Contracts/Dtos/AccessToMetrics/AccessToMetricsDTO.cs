namespace MetricService.Api.Contracts.Dtos.AccessToMetrics
{
    /// <summary>
    /// Объект данных о доступе к личным метрикам пользователя
    /// </summary>
    /// <seealso cref="AccessToMetricsBaseDTO" />
    public record AccessToMetricsDTO : AccessToMetricsBaseDTO
    {
        /// <summary>
        /// Идентификатор данных о доступе к личным метрикам пользователя
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Идентификатор пользователя, предоставляющий доступ к своим метрикам
        /// </summary>       
        public int ProviderUserId { get; init; }
    }
}
