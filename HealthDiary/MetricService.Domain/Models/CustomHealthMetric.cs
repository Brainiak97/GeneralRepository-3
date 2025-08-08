using MetricService.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Пользовательский показатель здоровья
    /// </summary>
    [Comment("Пользовательский показатель здоровья")]
    public class CustomHealthMetric : BaseModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>    
        [Comment("Идентификатор пользователя")]
        public int UserId { get; set; } // Идентификатор показателя

        /// <summary>
        /// Пользователь
        /// </summary>        
        public User User { get; set; } = null!;

        /// <summary>
        /// Наименование показателя (например, "Уровень стресса")
        /// </summary> 
        [Comment("Наименование показателя (например, \"Уровень стресса\")")]
        [MaxLength(150)]       
        public string Name { get; set; }=null!;

        /// <summary>
        /// Описание показателя (например, "Оценивайте стресс от 1 до 10")
        /// </summary> 
        [Comment("Описание показателя (например, \"Оценивайте стресс от 1 до 10\")")]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Единица измерения (шкала, проценты, абсолютные значения)
        /// </summary> 
        [Comment("Единица измерения (шкала, проценты, абсолютные значения)")]
        public MetricUnit Unit { get; set; }

        /// <summary>
        /// Дополнительные заметки пользователя
        /// </summary>  
        [Comment("Дополнительные заметки пользователя")]
        public string? Comment { get; set; }
    }
}
