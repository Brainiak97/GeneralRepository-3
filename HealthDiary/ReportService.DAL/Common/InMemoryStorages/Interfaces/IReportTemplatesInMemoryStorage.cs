using ReportService.Domain.Models.Entities;

namespace ReportService.DAL.Common.InMemoryStorages.Interfaces;

/// <summary>
/// InMemory хранилище данных по шаблонам отчётов.
/// </summary>
internal interface IReportTemplatesInMemoryStorage
{
    /// <summary>
    /// Вернуть данные шаблона отчёта по ключу.
    /// </summary>
    /// <param name="key">Ключ - идентификатор шаблона.</param>
    /// <param name="reportTemplateMetadata">Данные по шаблону отчёта.</param>
    /// <returns>Признак успешного получения шаблона отчёта по ключу.</returns>
    bool TryGetValue(int key, out ReportTemplateMetadata reportTemplateMetadata);    
}