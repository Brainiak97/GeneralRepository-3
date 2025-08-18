using ReportService.BLL.Common.Interfaces;

namespace ReportService.BLL.Common.Generators;

/// <summary>
/// Генератор отчёта.
/// </summary>
public interface IReportGenerator
{
    /// <summary>
    /// Сгенерировать отчёт.
    /// </summary>
    /// <param name="reportData">Содержимое отчёта.</param>
    /// <typeparam name="TData">Тип - хранилище данных для формирования отчёта.</typeparam>
    /// <returns>Сгенерированный отчёт.</returns>
    byte[] Generate<TData>(TData reportData) where TData : IReportData;
}