using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Запись пользовательских медицинских показателей
    /// </summary>
    [Comment("Запись пользовательских медицинских показателей")]
    public class CustomHealthMetricEntry : BaseModel
    {
        /// <summary>
        /// Ссылка на созданный пользователем показатель
        /// </summary> 
        [Comment("Ссылка на созданный пользователем показатель")]
        public int CustomHealthMetricId { get; set; }

        /// <summary>
        /// Пользовательский показатель здоровья
        /// </summary>  
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public CustomHealthMetric CustomHealthMetric { get; set; } = null!;

        /// <summary>
        /// Значение показателя
        /// </summary>  
        [Comment("Значение показателя")]
        public float Value { get; set; }

        /// <summary>
        /// Дата и время записи
        /// </summary>  
        [Comment("Дата и время записи")]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime RecordedAt { get; set; }

        /// <summary>
        /// Комментарий к записи
        /// </summary> 
        [Comment("Комментарий к записи")]
        public string? Comment { get; set; }
    }
}
