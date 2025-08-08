using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Физическая активность
    /// </summary>
    [Comment("Физическая активность")]
    public class PhysicalActivity : BaseModel
    {
        /// <summary>
        /// Наименование физической активности
        /// </summary>
        [Comment("Наименование физической активности")]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Метаболический эквивалент
        /// </summary>
        [Comment("Метаболический эквивалент")]
        public float EnergyEquivalent { get; set; }
    }
}
