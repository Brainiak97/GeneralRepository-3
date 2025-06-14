namespace MetricService.BLL.DTO.Workout
{
    public abstract class WorkoutBaseDTO
    {  
        /// <summary>
        /// Физическая активность
        /// </summary>
        public int PhysicalActivityId { get; set; }

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

        /// <summary>
        /// Потраченные калории за тренировку
        /// </summary>
        public float CaloriesBurned { get; internal set; }
    }
}
