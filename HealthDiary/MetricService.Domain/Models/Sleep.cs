using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Сон
    /// </summary>
    [Comment("Сон")]
    public class Sleep : BaseModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>  
        [Comment("идентификатор пользователя")]
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>        
        public User User { get; set; } = null!;

        /// <summary>
        /// Время начала сна
        /// </summary>  
        [Comment("время начала сна")]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime StartSleep { get; set; }

        /// <summary>
        /// Время окончания сна
        /// </summary> 
        [Comment("время окончания сна")]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime EndSleep { get; set; }

        /// <summary>
        /// Качество сна по 5-ой системе
        /// </summary>
        [Comment("качество сна по 5-ой системе")]
        public short QualityRating { get; set; }

        /// <summary>
        /// Примечания о качестве сна
        /// </summary>
        [Comment("примечания о качестве сна")]
        public string? Comment { get; set; }

        /// <summary>
        /// Длительность сна
        /// </summary>
        public TimeSpan SleepDuration => EndSleep - StartSleep;
    }
}
