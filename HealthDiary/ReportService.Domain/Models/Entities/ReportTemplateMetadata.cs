namespace ReportService.Domain.Models.Entities;

/// <summary>
/// Метаданные по шаблону отчёта.
/// </summary>
public class ReportTemplateMetadata
{
    /// <summary>
    /// Имя шаблона.
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Наименование типа источника данных для шаблона отчёта.
    /// </summary>
    public required Type DataSourceTypeName { get; set; }
}