using System.ComponentModel.DataAnnotations;

namespace MetricService.Api.Contracts.Dtos.AnalysisCategory
{
    /// <summary>
    /// Базовый объект данных для справочника "Категории анализов"
    /// </summary>
    public abstract record AnalysisCategoryBaseDTO
    {
        /// <summary>
        /// Наименование категории анализа(например, «Клинический анализ крови», «Биохимия»)
        /// </summary>       
        
        public required string Name { get; init; }


        /// <summary>
        /// Описание категории анализа
        /// </summary>
        public string? Description { get; init; }
    }
}
