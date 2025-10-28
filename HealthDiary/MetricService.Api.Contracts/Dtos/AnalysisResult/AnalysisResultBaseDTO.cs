namespace MetricService.Api.Contracts.Dtos.AnalysisResult
{
    /// <summary>
    /// Объект базовых данных результата анализа пользователя
    /// </summary>
    public abstract record AnalysisResultBaseDTO
    {
        /// <summary>
        /// Тип анализа
        /// </summary>       
        public int AnalysisTypeId { get; set; }


        /// <summary>
        /// Числовое значение результата анализа
        /// </summary>        
        public float? Value { get; set; }


        /// <summary>
        /// Развернутое описание исследования
        /// </summary>
        public string? DetailedResearchDescription { get; set; }

        /// <summary>
        /// Дата сдачи анализа
        /// </summary>        
        public DateTime TestedAt { get; set; }

        /// <summary>
        /// Любые заметки или замечания по этому анализу
        /// </summary>        
        public string? Comment { get; set; }
    }
}
