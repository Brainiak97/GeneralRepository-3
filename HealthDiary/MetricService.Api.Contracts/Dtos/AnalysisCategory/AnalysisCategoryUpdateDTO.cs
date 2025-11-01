namespace MetricService.Api.Contracts.Dtos.AnalysisCategory
{
    /// <summary>
    /// Объект для изменения данных в справочнике "Категории анализов"
    /// </summary>
    public record AnalysisCategoryUpdateDTO : AnalysisCategoryBaseDTO
    {
        /// <summary>
        /// Идентификатор данных в справочнике категории анализов
        /// </summary>
        public int Id { get; init; }
    }
}
