using MetricService.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Прием лекарств
    /// </summary>
    [Comment("Прием лекарств")]
    public class Intake : BaseModel
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
        /// Дата и время приема
        /// </summary>
        [Comment("Дата и время приема")]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime TakenAt { get; set; }

        /// <summary>
        /// Статусы приема (например, "принято", "пропущено", "перенесено")
        /// </summary> 
        [Comment("Статусы приема (например, \"принято\", \"пропущено\", \"перенесено\")")]
        [Column(TypeName = "smallint")]
        public IntakeStatus IntakeStatus { get; set; }

        /// <summary>
        /// Дополнительные заметки (например, причины пропуска)
        /// </summary> 
        [Comment("Дополнительные заметки (например, причины пропуска)")]
        public string? Comment { get; set; }
    }
}
