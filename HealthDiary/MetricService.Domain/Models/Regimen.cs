using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Схема приема медикаментов
    /// </summary>
    [Comment("Схема приема медикаментов")]
    public class Regimen : BaseModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary> 
        [Comment("Пользователь")]
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>    
        public User User { get; set; } = null!;

        /// <summary>
        /// Медицинский препарат
        /// </summary>
        [Comment("Медицинский препарат")]
        public int MedicationId { get; set; }

        /// <summary>
        /// Медицинский препарат
        /// </summary> 
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Medication Medication { get; set; } = null!;

        /// <summary>
        /// Прописанная дозировка (например, "1 табл." или "5 мл")
        /// </summary>  
        [Comment("Прописанная дозировка (например, \"1 табл.\" или \"5 мл\")")]
        public string Dosage { get; set; } = string.Empty;

        /// <summary>
        /// График приема (например, "Утро, обед, вечер")
        /// </summary> 
        [Comment("График приема (например, \"Утро, обед, вечер\")")]
        public string Shedule { get; set; } = string.Empty;

        /// <summary>
        /// Дата начала приема
        /// </summary> 
        [Comment("Дата начала приема")]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Предполагаемая дата окончания приема
        /// </summary> 
        [Comment("Предполагаемая дата окончания приема")]
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Заметки или дополнения
        /// </summary>  
        [Comment("Заметки или дополнения")]
        public string? Comment { get; set; }
    }
}