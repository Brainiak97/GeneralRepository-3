using ReportService.Domain.Models.Entities;
using Shared.Common.Interfaces;

namespace ReportService.DAL.Interfaces.Repositories;

/// <summary>
/// Предоставляет методы для работы с отчётами <see cref="Report"/> в хранилище данных.
/// </summary>
public interface IReportsRepository : IRepository<Report, int>
{
    /// <summary>
    /// Вернуть метаданные по шаблону отчёта.
    /// </summary>
    /// <param name="templateId">Идентификатор шаблона отчёта.</param>
    /// <returns>Возвращает шаблон отчёта или <see langword="null"/>, если нет.</returns>
    Task<ReportTemplateMetadata?> GetMetadataByIdAsync(int templateId);
}