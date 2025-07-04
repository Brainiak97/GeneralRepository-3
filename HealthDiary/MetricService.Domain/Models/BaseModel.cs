namespace MetricService.Domain.Models
{
    /// <summary>
    /// Базовая сущность, характеризующаяся идентификатром
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// идентификатор
        /// </summary>
        public int Id { get; set; }
    }
}
