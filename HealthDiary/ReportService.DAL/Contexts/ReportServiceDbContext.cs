using Microsoft.EntityFrameworkCore;
using ReportService.Domain.Models.Entities;

namespace ReportService.DAL.Contexts;

/// <summary>
/// Контекст базы данных для сервиса отчётов. 
/// </summary>
/// <remarks>
/// Инициализирует новый экземпляр контекста базы данных.
/// </remarks>
/// <param name="options">Настройки контекста базы данных.</param>
public class ReportServiceDbContext(DbContextOptions<ReportServiceDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Возвращает или устанавливает набор сущностей <see cref="Reports"/> в базе данных.
    /// </summary>
    public DbSet<Report> Reports { get; set; }

    /// <summary>
    /// Возвращает или устанавливает набор сущностей <see cref="ReportTemplatesMetadata"/> в базе данных.
    /// </summary>
    public DbSet<ReportTemplateMetadata> ReportTemplatesMetadata { get; set; }
}