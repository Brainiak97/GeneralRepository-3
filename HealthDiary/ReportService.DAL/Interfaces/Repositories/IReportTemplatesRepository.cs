using ReportService.Domain.Models.Entities;

namespace ReportService.DAL.Interfaces.Repositories;

/// <summary>
/// Предоставляет методы для работы с шаблонами отчётов <see cref="ReportTemplateMetadata"/> в хранилище данных.
/// </summary>
public interface IReportTemplatesRepository
{
    /// <summary>
    /// Вернуть шаблон отчёта.
    /// </summary>
    /// <param name="templateId">Идентификатор шаблона отчёта.</param>
    /// <returns>Возвращает шаблон отчёта или <see langword="null"/>, если нет.</returns>
    Task<ReportTemplateMetadata?> GetByIdAsync(int templateId);
}