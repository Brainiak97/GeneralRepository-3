namespace MetricService.Api.Contracts.Dtos.AnalysisCategory
{
    /// <summary>
    /// Объект для получения данных из справочника "Категории анализов"
    /// </summary>
    public record AnalysisCategoryDTO : AnalysisCategoryBaseDTO
    {
        /// <summary>
        /// Идентификатор данных в справочнике категорий анализов
        /// </summary>
        public int Id { get; init; }
    }
}
