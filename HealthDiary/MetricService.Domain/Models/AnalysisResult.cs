using Microsoft.EntityFrameworkCore;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Результаты анализов
    /// </summary>
    [Comment("Результаты анализов")]
    public class AnalysisResult : BaseModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Comment("Идентификатор пользователя")]
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>        
        public User User { get; set; } = null!;

        /// <summary>
        /// Идентификатор типа анализа
        /// </summary>  
        [Comment("Тип анализа")]
        public int AnalysisTypeId { get; set; }

        /// <summary>
        /// Тип анализа
        /// </summary>  
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public AnalysisType AnalysisType { get; set; } = null!;

        /// <summary>
        /// Числовое значение результата анализа
        /// </summary> 
        [Comment("Числовое значение результата анализа")]
        public float? Value { get; set; }

        /// <summary>
        /// Развернутое описание исследования
        /// </summary>
        [Comment("Развернутое описание исследования")]
        public string? DetailedResearchDescription { get; set; }

        /// <summary>
        /// Дата сдачи анализа
        /// </summary> 
        [Comment("Дата сдачи анализа")]
        public DateTime TestedAt { get; set; }

        /// <summary>
        /// Любые заметки или замечания по этому анализу
        /// </summary>  
        [Comment("Любые заметки или замечания по этому анализу")]
        public string? Comment { get; set; }
    }
}
