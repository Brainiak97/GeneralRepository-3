using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Медикаменты
    /// </summary>
    [Comment("Медикаменты")]
    public class Medication : BaseModel
    {
        /// <summary>
        /// Наименование препарата
        /// </summary> 
        [Comment("Наименование препарата")]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор формы выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary> 
        [Comment("Форма выпуска (таблетка, капсул, раствор и т.д.)")]
        public int DosageFormId { get; set; }

        /// <summary>
        /// Форма выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary> 
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public DosageForm DosageForm { get; set; } = null!;

        /// <summary>
        /// Инструкции по применению
        /// </summary> 
        [Comment("Инструкции по применению")]
        public string Instruction { get; set; } = string.Empty;
    }
}