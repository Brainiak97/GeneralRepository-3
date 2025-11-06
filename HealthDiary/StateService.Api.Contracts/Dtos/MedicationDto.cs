namespace StateService.Api.Contracts.Dtos
{
    /// <summary>
    /// Медикаменты
    /// </summary>    
    public record MedicationDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Наименование препарата
        /// </summary>         
        public required string Name { get; init; }

        /// <summary>
        /// Идентификатор формы выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>         
        public int DosageFormId { get; init; }

        /// <summary>
        /// Форма выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>         
        public DosageFormDto DosageForm { get; init; } = null!;

        /// <summary>
        /// Инструкции по применению
        /// </summary>         
        public required string Instruction { get; init; }
    }
}