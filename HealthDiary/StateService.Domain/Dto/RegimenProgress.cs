namespace StateService.Domain.Dto
{
    public class RegimenProgress
    {
        /// <summary>
        /// Идентификатор данных схемы приема медикаментов пользователем
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>       
        public int UserId { get; set; }

        /// <summary>
        /// Идентификатор данных из справочника "Медикаменты"
        /// </summary>       
        public int MedicationId { get; set; }

        public required Medication Medication { get; set; }

        /// <summary>
        /// Прописанная дозировка (например, "1 табл." или "5 мл")
        /// </summary>       
        public required string Dosage { get; set; }

        /// <summary>
        /// График приема (например, "Утро, обед, вечер")
        /// </summary>        
        public required string Shedule { get; set; }

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

        /// <summary>
        /// Прием медикаментов по схеме.
        /// </summary>        
        public required IEnumerable<Intake> Intakes { get; set; }
    }
}
