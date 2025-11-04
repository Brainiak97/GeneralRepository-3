namespace StateService.Domain.Dto
{
    public class Workout
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
        public float CaloriesBurned { get; set; }
    }
}
