using ReportService.Api.Contracts.Data.Responses;
using ReportService.Api.Contracts.Enums;

namespace ReportService.BLL.Interfaces;

/// <summary>
/// Предоставляет методы для работы с отчётами.
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Сгенерировать отчёт.
    /// </summary>
    /// <param name="appointmentResultId">Идентификатор результата приёма у врача.</param>
    /// <param name="reportFormat">Формат отчёта.</param>
    /// <returns>Ответ на запрос генерации отчёта.</returns>
    Task<GenerateReportResponse> GenerateReportAsync(int appointmentResultId, ReportFormat reportFormat);
}