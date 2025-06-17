namespace MetricService.BLL.DTO.AnalysisResult
{
    public class AnalysisResultDTO : AnalysisResultBaseDTO
    {

        /// <summary>
        /// идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }
    }
}
