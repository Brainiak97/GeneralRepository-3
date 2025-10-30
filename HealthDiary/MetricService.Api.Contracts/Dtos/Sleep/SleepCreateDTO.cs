namespace MetricService.Api.Contracts.Dtos.Sleep
{
    /// <summary>
    /// Объект для регистрации данных о сне пользователя
    /// </summary>
    public record SleepCreateDTO : SleepBaseDTO
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>        
        public int UserId { get; init; }
    }
}
