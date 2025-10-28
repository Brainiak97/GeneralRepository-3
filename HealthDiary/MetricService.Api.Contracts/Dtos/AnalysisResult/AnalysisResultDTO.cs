namespace MetricService.Api.Contracts.Dtos.AnalysisResult
{
    /// <summary>
    /// Объект данных результата анализа пользователя
    /// </summary>
    public record AnalysisResultDTO : AnalysisResultBaseDTO
    {

        /// <summary>
        /// Идентификатор анализа пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }
    }
}
