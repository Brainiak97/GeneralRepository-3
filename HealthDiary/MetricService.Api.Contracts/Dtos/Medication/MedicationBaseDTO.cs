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
        public required string Name { get; init; }

        /// <summary>
        /// Инструкции по применению
        /// </summary>        
        public required string Instruction { get; init; }
    }
}
