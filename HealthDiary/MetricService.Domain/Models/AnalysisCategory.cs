using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Категории анализов
    /// </summary>
    [Comment("Категории анализов")]
    public class AnalysisCategory : BaseModel
    {
        /// <summary>
        /// Наименование категории анализа(например, «Клинический анализ крови», «Биохимия»)
        /// </summary>
        [Comment("Наименование категории анализа")]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание категории анализа
        /// </summary>
        [Comment("Описание категории анализа")]
        public string? Description { get; set; }
    }
}
