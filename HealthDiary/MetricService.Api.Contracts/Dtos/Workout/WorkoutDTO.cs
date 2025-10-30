namespace MetricService.Api.Contracts.Dtos.Workout
{
    /// <summary>
    /// Объект данных о тренировке пользователя
    /// </summary>
    public record WorkoutDTO : WorkoutBaseDTO
    {
        /// <summary>
        /// Идентификатор данных о тренировке пользователя
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>        
        public int UserId { get; init; }
    }
}
