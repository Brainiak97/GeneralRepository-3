using static System.Net.Mime.MediaTypeNames;

namespace MetricService.Domain.Models
{

    /// <summary>
    /// Категории анализов
    /// </summary>
    public class AnalysisCategory : BaseModel
    {

        /// <summary>
        /// Наименование категории анализа(например, «Клинический анализ крови», «Биохимия»)
        /// </summary>
        public string Name { get; set; } = string.Empty;


        /// <summary>
        /// Описание категории анализа
        /// </summary>
        public string? Description { get; set; }

    }
}
