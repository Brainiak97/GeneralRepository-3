using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Форма выпуска препарата
    /// </summary>
    [Comment("Форма выпуска препарата")]
    public class DosageForm : BaseModel
    {
        /// <summary>
        /// Наименование формы выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>  
        [Comment("Наименование формы выпуска (таблетка, капсул, раствор и т.д.)")]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;
    }
}