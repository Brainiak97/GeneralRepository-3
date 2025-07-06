using MetricService.Domain.Models.Enums;

namespace MetricService.BLL.DTO.Intake
{
    /// <summary>
    /// Объект базовых данных о приеме лекарств пользователем
    /// </summary>
    public abstract class IntakeBaseDTO
    {
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
