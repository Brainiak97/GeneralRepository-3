namespace MetricService.BLL.DTO.PhysicalActivity
{
    /// <summary>
    /// Объект баховыъ данных для справочника "Физическая активность"
    /// </summary>
    public abstract class PhysicalActivityBaseDTO
    {

        /// <summary>
        /// Наименование физической активности
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Метаболический эквивалент
        /// </summary>
        public float EnergyEquivalent { get; set; }
    }
}
