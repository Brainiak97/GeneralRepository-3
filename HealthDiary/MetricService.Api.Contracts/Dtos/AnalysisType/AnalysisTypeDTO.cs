namespace MetricService.Api.Contracts.Dtos.AnalysisType
{
    /// <summary>
    /// Объект данных в справочнике "Типы анализов"
    /// </summary>
    public record AnalysisTypeDTO : AnalysisTypeBaseDTO
    {
        /// <summary>
        /// Идентификатор данных в справочнике
        /// </summary>
        public int Id { get; set; }
    }
}
