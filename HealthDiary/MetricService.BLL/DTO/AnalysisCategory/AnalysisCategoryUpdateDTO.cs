namespace MetricService.BLL.DTO.AnalysisCategory
{
    /// <summary>
    /// Объект для изменения данных в справочнике "Категории анализов"
    /// </summary>
    public class AnalysisCategoryUpdateDTO: AnalysisCategoryBaseDTO
    {
        /// <summary>
        /// Идентификатор данных в справочнике категории анализов
        /// </summary>
        public int Id { get; set; }
    }
}
