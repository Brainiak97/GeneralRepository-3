using ReportService.Api.Contracts.Enums;
using ReportService.BLL.Common.Generators;

namespace ReportService.BLL.Common.ReportFactory;

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