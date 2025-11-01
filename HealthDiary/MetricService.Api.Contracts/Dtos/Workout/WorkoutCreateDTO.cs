namespace MetricService.Api.Contracts.Dtos.Workout
{
    /// <summary>
    /// Объект для регистрации данных о тренировке пользователя
    /// </summary>
    public record WorkoutCreateDTO : WorkoutBaseDTO
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>        
        public int UserId { get; init; }
    }
}
