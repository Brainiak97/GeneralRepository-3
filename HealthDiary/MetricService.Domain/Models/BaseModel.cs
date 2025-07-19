using Microsoft.EntityFrameworkCore;

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
        [Comment("Идентификатор")]
        public int Id { get; set; }
    }
}
