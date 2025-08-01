using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Напоминание о приеме лекарств
    /// </summary>
    [Comment("Напоминание о приеме лекарств")]
    public class Reminder : BaseModel
    {
        /// <summary>
        /// Идентификатор схемы приема лекарств
        /// </summary> 
        [Comment("Схема приема лекарств")]
        public int RegimenId { get; set; }

        /// <summary>
        /// Схема приема лекарств
        /// </summary>
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Regimen Regimen { get; set; } = null!;

        /// <summary>
        /// Время напоминания
        /// </summary> 
        [Comment("Время напоминания")]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime RemindAt { get; set; }

        /// <summary>
        /// Признак, было ли отправлено напоминание
        /// </summary> 
        [Comment("Признак, было ли отправлено напоминание")]
        public bool IsSend { get; set; }
    }
}
