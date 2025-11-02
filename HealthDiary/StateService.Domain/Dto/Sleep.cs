namespace StateService.Domain.Dto
{
    public class Sleep
    {
        /// <summary>
        /// Время начала сна
        /// </summary>
        public DateTime StartSleep { get; set; }

        /// <summary>
        /// Время окончания сна
        /// </summary>        
        public DateTime EndSleep { get; set; }

        /// <summary>
        /// Качество сна по 5-ой системе
        /// </summary>
        public short QualityRating { get; set; }

        /// <summary>
        /// Примечания о качестве сна
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// Длительность сна
        /// </summary>
        public TimeSpan SleepDuration { get; set; }
    }
}
