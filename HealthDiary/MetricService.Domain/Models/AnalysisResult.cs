﻿namespace MetricService.Domain.Models
{

    /// <summary>
    /// Результаты анализов
    /// </summary>
    public class AnalysisResult : BaseModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>        
        public User User { get; set; } = null!;

        /// <summary>
        /// Идентификатор типа анализа
        /// </summary>       
        public int AnalysisTypeId { get; set; }

        /// <summary>
        /// Тип анализа
        /// </summary>       
        public AnalysisType AnalysisType { get; set; } = null!;


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
