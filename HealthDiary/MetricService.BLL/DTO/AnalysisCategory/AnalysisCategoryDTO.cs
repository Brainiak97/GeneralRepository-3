namespace MetricService.BLL.DTO.AnalysisCategory
{
    /// <summary>
    /// Объект для получения данных из справочника "Категории анализов"
    /// </summary>
    public class AnalysisCategoryDTO: AnalysisCategoryCreateDTO
    { 
        /// <summary>
        /// Идентификатор данных в справочнике категорий анализов
        /// </summary>
        public int Id { get; set; }       
    }   
}
