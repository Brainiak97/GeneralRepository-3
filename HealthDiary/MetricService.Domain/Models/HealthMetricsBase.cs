using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Базовые медицинские показатели
    /// </summary>
    [Comment("Базовые медицинские показатели")]
    public class HealthMetricsBase : BaseModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary> 
        [Comment("Идентификатор пользователя")]
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>        
        public User User { get; set; } = null!;

        /// <summary>
        /// Дата замера показателя
        /// </summary>  
        [Comment("Дата замера показателя")]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime MetricDate { get; set; }

        /// <summary>
        /// Частота сердечных сокращений (ударов/мин)
        /// </summary>  
        [Comment("Частота сердечных сокращений (ударов/мин)")]
        public short HeartRate { get; set; }

        /// <summary>
        /// Верхнее артериальное давление (мм рт. ст.)
        /// </summary>
        [Comment("Верхнее артериальное давление (мм рт. ст.)")]
        public short? BloodPressureSys { get; set; }

        /// <summary>
        /// Нижнее артериальное давление (мм рт. ст.)
        /// </summary>
        [Comment("Нижнее артериальное давление (мм рт. ст.)")]
        public short? BloodPressureDia { get; set; }

        /// <summary>
        /// Процент жира в организме
        /// </summary>
        [Comment("Процент жира в организме")]
        public float? BodyFatPercentage { get; set; }

        /// <summary>
        /// Потребление воды (мл)
        /// </summary>
        [Comment("Потребление воды (мл)")]
        public short? WaterIntake { get; set; }
    }
}
