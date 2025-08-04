using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Типы анализов
    /// </summary>
    [Comment("Типы анализов")]
    public class AnalysisType : BaseModel
    {
        /// <summary>
        /// Идентификатор категории анализа
        /// </summary>
        [Comment("Ссылка на категорию анализа")]
        public int AnalysisCategoryId { get; set; }

        /// <summary>
        /// Категория анализа
        /// </summary>
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public AnalysisCategory AnalysisCategory { get; set; } = null!;

        /// <summary>
        /// Название конкретного анализа(например, «Лейкоциты», «Холестерин»)
        /// </summary>
        [Comment("Название конкретного анализа(например, «Лейкоциты», «Холестерин»)")]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        ///Эталонное значение мужской
        /// </summary> 
        [Comment("Эталонное значение мужской")]
        [MaxLength(150)]
        public string? ReferenceValueMale { get; set; }

        /// <summary>
        ///Эталонное значение женский
        /// </summary>  
        [Comment("Эталонное значение женский")]
        [MaxLength(150)]
        public string? ReferenceValueFemale { get; set; }

        /// <summary>
        /// Единица измерения(например, г/л, ммоль/л)
        /// </summary>        
        public string? Unit { get; set; }
    }
}
