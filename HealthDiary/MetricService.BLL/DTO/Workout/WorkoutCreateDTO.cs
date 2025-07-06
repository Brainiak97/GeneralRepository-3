namespace MetricService.BLL.DTO.Workout
{
    /// <summary>
    /// Объект для регистрации данных о тренировке пользователя
    /// </summary>
    public class WorkoutCreateDTO: WorkoutBaseDTO
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>        
        public int UserId { get; set; }
    }
}
