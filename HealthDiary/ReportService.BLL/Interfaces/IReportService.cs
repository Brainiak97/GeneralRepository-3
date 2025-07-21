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
    /// <param name="data">Данные для генерации отчёта.</param>
    /// <param name="format">Формат отчёта.</param>
    /// <typeparam name="TData">Тип данных отчёта для генерации.</typeparam>
    void GenerateReport<TData>(TData data, ReportFormat format);
}