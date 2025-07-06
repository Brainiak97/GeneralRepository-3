namespace MetricService.Domain.Models
{
    /// <summary>
    /// Схема приема медикаментов
    /// </summary>
    public class Regimen: BaseModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>       
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>    
        public User User { get; set; } = null!;

        /// <summary>
        /// Медицинский препарат
        /// </summary>       
        public int MedicationId { get; set; }

        /// <summary>
        /// Медицинский препарат
        /// </summary>       
        public Medication Medication { get; set; } = null!;

        /// <summary>
        /// Прописанная дозировка (например, "1 табл." или "5 мл")
        /// </summary>       
        public string Dosage { get; set; } = string.Empty;

        /// <summary>
        /// График приема (например, "Утро, обед, вечер")
        /// </summary>        
        public string Shedule { get; set; } = string.Empty;

        /// <summary>
        /// Дата начала приема
        /// </summary>        
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Предполагаемая дата окончания приема
        /// </summary>        
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Заметки или дополнения
        /// </summary>        
        public string? Comment { get; set; }
    }
}
