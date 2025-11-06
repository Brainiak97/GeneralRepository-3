using ReportService.BLL.Data;
using ReportService.BLL.Data.Commands;
using ReportService.Domain.Models.Entities;

namespace ReportService.BLL.Interfaces;

/// <summary>
/// Предоставляет методы для работы с отчётами.
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Сгенерировать отчёт.
    /// </summary>
    /// <param name="command">Идентификатор результата приёма у врача.</param>
    /// <param name="cancellationToken">Формат отчёта.</param>
    /// <returns>Сгенерированный отчёт.</returns>
    Task<(byte[] Content, string FileName)> GenerateReportAsync(GenerateReportCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Вернуть отчёт по идентификатору.
    /// </summary>
    /// <param name="reportId">Идентификатор отчёта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Отчёт по идентификатору или <see langword="null"/> если нет.</returns>
    Task<Report?> GetReportByIdAsync(int reportId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить отчёт.
    /// </summary>
    /// <param name="command">Команда добавления сгенерированного отчёта.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Идентификатор отчёта.</returns>
    Task<int> AddReportAsync(AddReportCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Редактировать отчёт.
    /// </summary>
    /// <param name="command">Команда редактирования сгенерированного отчёта.</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task"/>.</returns>
    Task UpdateReportAsync(UpdateReportCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить отчёт.
    /// </summary>
    /// <param name="reportId">Идентификатор отчёта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task DeleteReportAsync(int reportId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Вернуть тип шаблонов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Все шаблоны отчётов.</returns>
    Task<ReportTemplateType[]> GetReportTemplateTypesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Вернуть шаблон отчёта по идентификатору.
    /// </summary>
    /// <param name="templateId">Идентификатор шаблона.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Шаблон отчёта.</returns>
    Task<List<TemplateField>> GetReportTemplateByIdAsync(int templateId, CancellationToken cancellationToken = default);
}