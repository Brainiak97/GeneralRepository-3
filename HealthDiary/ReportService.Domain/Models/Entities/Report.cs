using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Interfaces;

namespace ReportService.Domain.Models.Entities;

/// <summary>
/// Отчёт.
/// </summary>
[Comment("Отчёты")]
public class Report : IEntityModel<int>
{
    /// <summary>
    /// Идентификатор отчёта.
    /// </summary>
    [Comment("Идентификатор отчёта")]
    public int Id { get; set; }

    /// <summary>
    /// Имя файла.
    /// </summary>
    [Comment("Имя файла")]
    [Required]
    [MaxLength(255)]
    public string FileName { get; set; } = null!;
    
    /// <summary>
    /// Формат отчёта.
    /// </summary>
    [Comment("Формат отчёта")]
    public ReportFormat ReportFormat { get; set; }

    /// <summary>
    /// Содержимое отчёта.
    /// </summary>
    [Comment("Содержимое отчёта")]
    public required byte[] Content { get; set; }
}