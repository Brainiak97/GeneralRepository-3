namespace MetricService.BLL.DTO.AnalysisResult
{
    /// <summary>
    /// Объект для изменения данных результата анализа пользователя
    /// </summary>
    public class AnalysisResultUpdateDTO : AnalysisResultBaseDTO
    {
        /// <summary>
        /// Идентификатор результата анализа
        /// </summary>
        public int Id { get; set; }
    }
}
