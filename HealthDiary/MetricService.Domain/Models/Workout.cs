namespace MetricService.Domain.Models
{
    /// <summary>
    /// Тренировка
    /// </summary>
    public class Workout : BaseModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>        
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>        
        public User User { get; set; } = null!;

        /// <summary>
        /// Идентификатор физической активности
        /// </summary>
        public int PhysicalActivityId { get; set; }

        /// <summary>
        /// Физическая активность
        /// </summary>
        public PhysicalActivity PhysicalActivity { get; set; } = null!;

        /// <summary>
        /// Время начала тренировки
        /// </summary>        
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Время окончания тренировки
        /// </summary>       
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Потраченные калории за тренировку
        /// </summary>
        public float CaloriesBurned =>
            (float)(PhysicalActivity.EnergyEquivalent * User.Weight * (EndTime - StartTime).Hours);
    }
}
