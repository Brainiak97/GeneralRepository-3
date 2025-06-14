namespace MetricService.Domain.Models
{
    /// <summary>
    /// тренировка
    /// </summary>
    public class Workout : BaseModel
    {
        /// <summary>
        /// пользователя
        /// </summary>        
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>        
        public User User { get; set; } = null!;

        /// <summary>
        /// Физическая активность
        /// </summary>
        public int PhysicalActivityId { get; set; }

        /// <summary>
        /// Физическая активность
        /// </summary>
        public PhysicalActivity PhysicalActivity { get; set; } = null!;

        /// <summary>
        /// время начала тренировки
        /// </summary>        
        public DateTime StartTime { get; set; }

        /// <summary>
        /// время окончания тренировки
        /// </summary>       
        public DateTime EndTime { get; set; }

        /// <summary>
        /// описание
        /// </summary>
        public string? Description { get; set; }

        public float CaloriesBurned => 
            (float) (PhysicalActivity.EnergyEquivalent* User.Weight* (EndTime - StartTime).Hours);
    }
}
