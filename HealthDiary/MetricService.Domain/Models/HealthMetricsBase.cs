using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Показатель здоровья пользователя
    /// </summary>
    [Comment("Показатель здоровья пользователя")]
    public class HealthMetricsBase_удалить : BaseModel
    {
        /// <summary>
        /// Наименование показателя (например, "Уровень стресса")
        /// </summary> 
        [Comment("Наименование показателя (например, \"Уровень стресса\")")]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Описание показателя (например, "Оценивайте стресс от 1 до 10")
        /// </summary> 
        [Comment("Описание показателя (например, \"Оценивайте стресс от 1 до 10\")")]
        public string? Description { get; set; }

        /// <summary>
        /// Единица измерения (шкала, проценты, абсолютные значения)
        /// </summary> 
        [Comment("Единица измерения (кг., мм.рт.ст., % и т.д.)")]
        [MaxLength(50)]
        public string? Unit { get; set; }
    }
}
