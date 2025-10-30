namespace MetricService.Api.Contracts.Dtos.User
{
    /// <summary>
    /// Объект данных о профиле пользователя
    /// </summary>
    public record UserDTO
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Рост в сантиметрах
        /// </summary>
        public short Height { get; init; }

        /// <summary>
        /// Вес в килограммах
        /// </summary>
        public float Weight { get; init; }
    }
}
