using Microsoft.EntityFrameworkCore;
using Shared.Common.Interfaces;

namespace ReportService.Domain.Models.Entities;

/// <summary>
/// Метаданные по шаблону отчёта.
/// </summary>
[Comment("Метаданные по шаблонам отчётов")]
public class ReportTemplateMetadata : IEntityModel<int>
{
    /// <summary>
    /// Идентификатор шаблона.
    /// </summary>
    [Comment("Идентификатор шаблона")]
    public int Id { get; set; }
    
    /// <summary>
    /// Имя шаблона.
    /// </summary>
    [Comment("Имя шаблона")]
    public required string Name { get; set; }

    /// <summary>
    /// Наименование типа шаблона в приложении.
    /// </summary>
    [Comment("Наименование типа шаблона в приложении")]
    public required string ReportTemplateTypeName { get; set; }

    /// <summary>
    /// Наименование типа источника данных для шаблона отчёта в приложении.
    /// </summary>
    [Comment("Наименование типа источника данных для шаблона отчёта в приложении")]
    public required string ReportTemplateSourceTypeName { get; set; }
}