using ReportService.Api.Contracts.Enums;

namespace ReportService.BLL.Common.Generators.Containers;

/// <summary>
/// Контейнер с генераторами отчётов.
/// </summary>
public interface IReportGeneratorsContainer
{
    /// <summary>
    /// Получить генератор отчёта.
    /// </summary>
    /// <param name="reportFormat">Формат отчёта.</param>
    /// <returns>Генератор отчёта.</returns>
    IReportGenerator GetGenerator(ReportFormat reportFormat);
}