namespace MetricService.BLL.Dto
{
    public  class WorkoutDTO
    {
        /// <summary>
        /// идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// пользователя
        /// </summary>        
        public int UserId { get; set; }
     
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
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// Потраченные калории за тренировку
        /// </summary>
        public float CaloriesBurned { get; internal set; }
    }
}
