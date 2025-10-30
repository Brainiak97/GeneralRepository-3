namespace MetricService.Api.Contracts.Dtos.Sleep
{
    /// <summary>
    /// Объект для изменения данных о сне пользователя
    /// </summary>
    public record SleepUpdateDTO : SleepBaseDTO
    {
        /// <summary>
        /// Идентификатор данны о сне пользователя        
        /// </summary>    
        public int Id { get; init; }
    }
}
