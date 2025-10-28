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
        public string? Comment { get; set; } = string.Empty;
    }
}
