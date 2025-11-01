namespace MetricService.Api.Contracts.Dtos.DosageForm
{
    /// <summary>
    /// Объект базовых данных в справочнике "Форма выпуска препарата"
    /// </summary>
    public abstract record DosageFormBaseDTO
    {
        /// <summary>
        /// Наименование формы выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>    
        public required string Name { get; init; }
    }
}
