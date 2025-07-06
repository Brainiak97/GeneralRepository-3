namespace MetricService.BLL.DTO.AnalysisType
{
    /// <summary>
    /// Объект для изменения данных в справочнике "Типы анализов"
    /// </summary>
    public class AnalysisTypeUpdateDTO : AnalysisTypeBaseDTO
    {
        /// <summary>
        /// Идентификатор данных в справочнике
        /// </summary>
        public int Id { get; set; }
    }
}
