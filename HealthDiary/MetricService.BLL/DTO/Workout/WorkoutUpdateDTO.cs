namespace MetricService.BLL.DTO.Workout
{
    /// <summary>
    /// Объект для изменения данных о тренировке пользователя
    /// </summary>
    public class WorkoutUpdateDTO: WorkoutBaseDTO
    {
        /// <summary>
        /// Идентификатор данных о тренировке пользователя
        /// </summary>
        public int Id { get; set; } 
    }
}
