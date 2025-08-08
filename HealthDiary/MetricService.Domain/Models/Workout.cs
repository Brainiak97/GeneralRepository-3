using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Тренировка
    /// </summary>
    [Comment("Тренировки")]
    public class Workout : BaseModel
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
        /// Идентификатор физической активности
        /// </summary>
        [Comment("Физ. активность")]
        public int PhysicalActivityId { get; set; }

        /// <summary>
        /// Физическая активность
        /// </summary>
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public PhysicalActivity PhysicalActivity { get; set; } = null!;

        /// <summary>
        /// Время начала тренировки
        /// </summary>    
        [Comment("Время начала тренировки")]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Время окончания тренировки
        /// </summary>  
        [Comment("Время окончания тренировки")]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [Comment("Описание")]
        public string? Description { get; set; }

        /// <summary>
        /// Потраченные калории за тренировку
        /// </summary>
        public float CaloriesBurned =>
            (float)(PhysicalActivity.EnergyEquivalent * User.Weight * (EndTime - StartTime).Hours);
    }
}
