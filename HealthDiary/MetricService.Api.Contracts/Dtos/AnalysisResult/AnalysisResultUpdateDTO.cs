namespace MetricService.Api.Contracts.Dtos.AnalysisResult
{
    /// <summary>
    /// Объект для изменения данных результата анализа пользователя
    /// </summary>
    public record AnalysisResultUpdateDTO : AnalysisResultBaseDTO
    {
        /// <summary>
        /// Идентификатор результата анализа
        /// </summary>
        public int Id { get; set; }
    }
}
