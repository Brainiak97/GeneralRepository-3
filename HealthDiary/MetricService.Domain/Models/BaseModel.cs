namespace MetricService.Domain.Models
{
    /// <summary>
    /// Базовая сущность, характеризующаяся идентификатором
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public int Id { get; set; }
    }
}
