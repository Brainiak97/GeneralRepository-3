using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Значение показателя здоровья пользователя
    /// </summary>
    [Comment("Значение показателя здоровья пользователя")]
    public class HealthMetricValue : BaseModel
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
        /// Ссылка на показатель здоровья
        /// </summary> 
        [Comment("Ссылка на созданный пользователем показатель")]
        public int HealthMetricId { get; set; }

        /// <summary>
        /// Показатель здоровья
        /// </summary>  
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public HealthMetric HealthMetric { get; set; } = null!;

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
