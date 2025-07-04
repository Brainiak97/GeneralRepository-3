using MetricService.Domain.Models.Enums;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Прием лекарств
    /// </summary>
    public class Intake : BaseModel
    {
        /// <summary>
        /// Схема приема лекарств
        /// </summary>        
        public int RegimenId { get; set; }

        /// <summary>
        /// Схема приема лекарств
        /// </summary>  
        public Regimen Regimen { get; set; } = null!;

        /// <summary>
        /// Дата и время приема
        /// </summary>        
        public DateTime TakenAt { get; set; }

        /// <summary>
        /// Статусы приема (например, "принято", "пропущено", "перенесено")
        /// </summary>       
        public IntakeStatus IntakeStatus { get; set; }

        /// <summary>
        /// дополнительные заметки (например, причины пропуска)
        /// </summary>        
        public string? Comment { get; set; }
    }
}
