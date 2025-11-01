namespace MetricService.Api.Contracts.Dtos.AccessToMetrics
{
    /// <summary>
    /// Объект для изменения доступа к личным метрикам пользователя
    /// </summary>
    /// <seealso cref="AccessToMetricsBaseDTO" />
    public record AccessToMetricsUpdateDTO : AccessToMetricsBaseDTO
    {
        /// <summary>
        /// Идентификатор данных о доступе к личным метрикам пользователя
        /// </summary>
        public int Id { get; init; }       
    }
}
