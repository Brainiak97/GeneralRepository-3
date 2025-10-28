namespace MetricService.Api.Contracts.Dtos.Medication
{
    /// <summary>
    /// Объект базовых данных для справочника "Медикаменты"
    /// </summary>
    public record MedicationBaseDTO
    {
        /// <summary>
        /// Наименование препарата
        /// </summary>        
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Инструкции по применению
        /// </summary>        
        public string Instruction { get; set; } = string.Empty;
    }
}
