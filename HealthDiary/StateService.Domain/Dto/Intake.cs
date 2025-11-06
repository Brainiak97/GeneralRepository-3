using StateService.Domain.Dto.Enums;

namespace StateService.Domain.Dto
{
    /// <summary>
    /// Прием лекарств
    /// </summary>    
    public class Intake
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор схемы приема лекарств
        /// </summary>         
        public int RegimenId { get; set; } 

        /// <summary>
        /// Дата и время приема
        /// </summary>        
        public DateTime TakenAt { get; set; }

        /// <summary>
        /// Статусы приема (например, "принято", "пропущено", "перенесено")
        /// </summary>        
        public IntakeStatus IntakeStatus { get; set; }

        /// <summary>
        /// Дополнительные заметки (например, причины пропуска)
        /// </summary>         
        public string? Comment { get; set; }
    }
}