namespace StateService.Api.Contracts.Dtos
{
    /// <summary>
    /// Объект данных схемы приема медикаментов пользователем
    /// </summary>
    public record RegimenProgressDto
    {
        /// <summary>
        /// Идентификатор данных схемы приема медикаментов пользователем
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>       
        public int UserId { get; init; }

        /// <summary>
        /// Идентификатор данных из справочника "Медикаменты"
        /// </summary>       
        public int MedicationId { get; init; }

        public required MedicationDto Medication { get; init; }

        /// <summary>
        /// Прописанная дозировка (например, "1 табл." или "5 мл")
        /// </summary>       
        public required string Dosage { get; init; }

        /// <summary>
        /// График приема (например, "Утро, обед, вечер")
        /// </summary>        
        public required string Shedule { get; init; }

        /// <summary>
        /// Дата начала приема
        /// </summary>        
        public DateTime StartDate { get; init; }

        /// <summary>
        /// Предполагаемая дата окончания приема
        /// </summary>        
        public DateTime? EndDate { get; init; }

        /// <summary>
        /// Заметки или дополнения
        /// </summary>        
        public string? Comment { get; init; }

        /// <summary>
        /// Приемы лекарств по схеме
        /// </summary>       
        public required IEnumerable<IntakeDto> Intakes { get; init; }
    }
}
