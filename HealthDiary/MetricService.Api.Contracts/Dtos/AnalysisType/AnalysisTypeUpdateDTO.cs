namespace MetricService.Api.Contracts.Dtos.AnalysisType
{
    /// <summary>
    /// Объект для изменения данных в справочнике "Типы анализов"
    /// </summary>
    public record AnalysisTypeUpdateDTO : AnalysisTypeBaseDTO
    {
        /// <summary>
        /// Идентификатор данных в справочнике
        /// </summary>
        public int Id { get; set; }
    }
}
