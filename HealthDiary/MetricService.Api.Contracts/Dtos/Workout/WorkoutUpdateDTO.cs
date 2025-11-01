namespace MetricService.Api.Contracts.Dtos.Workout
{
    /// <summary>
    /// Объект для изменения данных о тренировке пользователя
    /// </summary>
    public record WorkoutUpdateDTO : WorkoutBaseDTO
    {
        /// <summary>
        /// Идентификатор данных о тренировке пользователя
        /// </summary>
        public int Id { get; init; }
    }
}
