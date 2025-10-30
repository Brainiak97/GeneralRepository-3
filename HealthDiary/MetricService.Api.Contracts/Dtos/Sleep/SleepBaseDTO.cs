namespace MetricService.Api.Contracts.Dtos.Sleep
{
    /// <summary>
    /// Объект базовых данных о сне пользователя
    /// </summary>
    public abstract record SleepBaseDTO
    {
        /// <summary>
        /// Время начала сна
        /// </summary>
        public DateTime StartSleep { get; init; }

        /// <summary>
        /// Время окончания сна
        /// </summary>        
        public DateTime EndSleep { get; init; }

        /// <summary>
        /// Качество сна по 5-ой системе
        /// </summary>
        public short QualityRating { get; init; }

        /// <summary>
        /// Примечания о качестве сна
        /// </summary>
        public string? Comment { get; init; }

        /// <summary>
        /// Длительность сна
        /// </summary>
        public TimeSpan SleepDuration { get; init; }
    }
}
