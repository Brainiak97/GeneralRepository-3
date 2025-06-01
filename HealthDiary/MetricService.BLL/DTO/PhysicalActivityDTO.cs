namespace MetricService.BLL.Dto
{
    public class PhysicalActivityDTO
    {
        /// <summary>
        /// идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование физической активности
        /// </summary>
        public string Name { get; set;  } = string.Empty;

        /// <summary>
        /// Метаболический эквивалент
        /// </summary>
        public float EnergyEquivalent { get; set; }
    }
}
