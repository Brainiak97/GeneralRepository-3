namespace MetricService.Api.Contracts.Dtos.AnalysisResult
{
    /// <summary>
    /// Объект для регистрации данных результата анализа пользователя
    /// </summary>
    public record AnalysisResultCreateDTO : AnalysisResultBaseDTO
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; init; }
    }
}
