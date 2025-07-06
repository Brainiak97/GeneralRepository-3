namespace MetricService.BLL.DTO.AnalysisResult
{
    /// <summary>
    /// Объект для регистрации данных результата анализа пользователя
    /// </summary>
    public class AnalysisResultCreateDTO : AnalysisResultBaseDTO
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }
    }
}
