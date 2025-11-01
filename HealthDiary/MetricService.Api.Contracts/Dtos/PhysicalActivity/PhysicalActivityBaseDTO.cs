namespace MetricService.Api.Contracts.Dtos.PhysicalActivity
{
    /// <summary>
    /// Объект баховыъ данных для справочника "Физическая активность"
    /// </summary>
    public abstract record PhysicalActivityBaseDTO
    {

        /// <summary>
        /// Наименование физической активности
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Метаболический эквивалент
        /// </summary>
        public float EnergyEquivalent { get; init; }
    }
}
