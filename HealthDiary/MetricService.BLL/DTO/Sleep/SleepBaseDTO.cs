namespace MetricService.BLL.DTO.Sleep
{
    public abstract class SleepBaseDTO
    {        
        public DateTime StartSleep { get; set; }

        /// <summary>
        /// время окончания сна
        /// </summary>        
        public DateTime EndSleep { get; set; }

        /// <summary>
        /// качество сна по 5-ой системе
        /// </summary>
        public short QualityRating { get; set; }

        /// <summary>
        /// примечания о качестве сна
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// длительность сна
        /// </summary>
        public TimeSpan SleepDuration { get; internal set; }
    }

}
