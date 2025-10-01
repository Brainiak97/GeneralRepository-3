using ReportService.Api.Contracts.Enums;
using ReportService.BLL.Common.Generators.Pdf;

namespace ReportService.BLL.Common.Generators.Containers;

/// <inheritdoc />
internal class ReportGeneratorsContainer(IPdfReportGenerator pdfReportGenerator) : IReportGeneratorsContainer
{
    /// <summary>
    /// Генераторы отчётов.
    /// </summary>
    private readonly IReadOnlyDictionary<ReportFormat, IReportGenerator> _reportGenerators =
        new Dictionary<ReportFormat, IReportGenerator>
        {
            { ReportFormat.Pdf, pdfReportGenerator },
        };

    /// <inheritdoc />
    public IReportGenerator GetGenerator(ReportFormat reportFormat) =>
        _reportGenerators.TryGetValue(reportFormat, out var generator)
            ? generator
            : throw new InvalidOperationException($"Не найден генератор для отчётов формата {reportFormat}");
}