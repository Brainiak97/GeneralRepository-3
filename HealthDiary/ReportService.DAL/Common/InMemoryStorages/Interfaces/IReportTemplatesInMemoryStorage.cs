using ReportService.Domain.Models.Entities;

namespace ReportService.DAL.Common.InMemoryStorages.Interfaces;

/// <summary>
/// InMemory хранилище данных по шаблонам отчётов.
/// </summary>
internal interface IReportTemplatesInMemoryStorage
{
    /// <summary>
    /// Вернуть метаданные шаблона отчёта по ключу.
    /// </summary>
    /// <param name="key">Ключ - идентификатор шаблона.</param>
    /// <returns>Метаданные по шаблону отчёта.</returns>
    ReportTemplateMetadata? GetByKey(int key);
}