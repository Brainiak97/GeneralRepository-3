using ReportService.Domain.Models;

namespace ReportService.BLL.Common.Generators.Factories;

/// <summary>
/// Фабрика генераторов отчётов.
/// </summary>
public interface IReportGeneratorFactory
{
    /// <summary>
    /// Создать генератор отчёта.
    /// </summary>
    /// <param name="reportFormat">Формат отчёта.</param>
    /// <returns>Генератор отчёта.</returns>
    IReportGenerator CreateGenerator(ReportFormat reportFormat);
}