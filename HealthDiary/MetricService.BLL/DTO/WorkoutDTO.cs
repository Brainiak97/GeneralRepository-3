using MetricService.Domain.Models;

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
        public string? Description { get; set; } = string.Empty;
    }
}
