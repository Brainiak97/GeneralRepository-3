namespace MetricService.Api.Contracts.Dtos.Regimen
{
    /// <summary>
    /// Объект базовых данных о схеме приема меликаментов пользователем
    /// </summary>
    public abstract record RegimenBaseDTO
    {
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
    }
}
