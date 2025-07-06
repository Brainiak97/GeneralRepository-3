namespace MetricService.BLL.DTO.AnalysisResult
{
    /// <summary>
    /// Объект данных результата анализа пользователя
    /// </summary>
    public class AnalysisResultDTO : AnalysisResultBaseDTO
    {

        /// <summary>
        /// Идентификатор анализа пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }
    }
}
