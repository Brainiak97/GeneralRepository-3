namespace MetricService.Domain.Models
{
    /// <summary>
    /// Физическая активность
    /// </summary>
    public class PhysicalActivity: BaseModel
    {
        /// <summary>
        /// идентификатор
        /// </summary>
        public new int Id { get; set; }

        /// <summary>
        /// Наименование физической активности
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Метаболический эквивалент
        /// </summary>
        public float EnergyEquivalent { get; }
    }
}
