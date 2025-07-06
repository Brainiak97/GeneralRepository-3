namespace MetricService.Domain.Models
{
    /// <summary>
    /// Напоминание о приеме лекарств
    /// </summary>
    public class Reminder : BaseModel
    {
        /// <summary>
        /// Идентификатор схемы приема лекарств
        /// </summary>        
        public int RegimenId { get; set; }

        /// <summary>
        /// Схема приема лекарств
        /// </summary>   
        public Regimen Regimen { get; set; } = null!;

        /// <summary>
        /// Время напоминания
        /// </summary>       
        public DateTime RemindAt { get; set; }

        /// <summary>
        /// Признак, было ли отправлено напоминание
        /// </summary>        
        public bool IsSend { get; set; }
    }
}
