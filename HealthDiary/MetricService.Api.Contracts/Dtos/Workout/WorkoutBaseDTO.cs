namespace MetricService.Api.Contracts.Dtos.Workout
{
    /// <summary>
    /// Объект базовых данных о тренировке пользователя
    /// </summary>
    public abstract record WorkoutBaseDTO
    {
        /// <summary>
        /// Идентификатор данных и справочника "Физическая активность"
        /// </summary>
        public int PhysicalActivityId { get; set; }

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
        public float CaloriesBurned { get; internal set; }
    }
}
