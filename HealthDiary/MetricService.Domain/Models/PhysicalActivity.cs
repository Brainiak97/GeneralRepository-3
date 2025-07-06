namespace MetricService.Domain.Models
{
    /// <summary>
    /// Физическая активность
    /// </summary>
    public class PhysicalActivity: BaseModel
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
