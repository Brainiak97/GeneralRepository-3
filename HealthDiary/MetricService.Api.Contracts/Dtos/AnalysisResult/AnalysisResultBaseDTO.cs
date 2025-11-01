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
        public int AnalysisTypeId { get; init; }


        /// <summary>
        /// Числовое значение результата анализа
        /// </summary>        
        public float? Value { get; init; }


        /// <summary>
        /// Развернутое описание исследования
        /// </summary>
        public string? DetailedResearchDescription { get; init; }

        /// <summary>
        /// Дата сдачи анализа
        /// </summary>        
        public DateTime TestedAt { get; init; }

        /// <summary>
        /// Любые заметки или замечания по этому анализу
        /// </summary>        
        public string? Comment { get; init; }
    }
}
