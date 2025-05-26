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
        public new int Id { get; }

        /// <summary>
        /// Наименование физической активности
        /// </summary>
        public string Name { get; } = string.Empty;

        /// <summary>
        /// Метаболический эквивалент
        /// </summary>
        public float EnergyEquivalent { get; }
    }
}
