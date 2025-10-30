namespace MetricService.Api.Contracts.Dtos.Sleep
{
    /// <summary>
    /// Объект данных о сне пользователя
    /// </summary>
    public record SleepDTO : SleepBaseDTO
    {
        /// <summary>
        /// Идентификатор данны о сне пользователя
        /// </summary>    
        public int Id { get; init; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>        
        public int UserId { get; init; }
    }
}
