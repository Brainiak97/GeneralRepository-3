namespace MetricService.BLL.DTO.Workout
{
    /// <summary>
    /// Объект данных о тренировке пользователя
    /// </summary>
    public  class WorkoutDTO:WorkoutBaseDTO
    {
        /// <summary>
        /// Идентификатор данных о тренировке пользователя
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>        
        public int UserId { get; set; }
    }
}
