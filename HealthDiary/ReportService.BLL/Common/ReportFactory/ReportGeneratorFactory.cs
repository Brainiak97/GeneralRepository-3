using ReportService.Api.Contracts.Enums;
using ReportService.BLL.Common.Generators;
using ReportService.BLL.Common.Generators.Pdf;

namespace ReportService.BLL.Common.ReportFactory;

/// <inheritdoc />
internal class ReportGeneratorFactory(IPdfReportGenerator pdfReportGenerator) : IReportGeneratorFactory
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
    public IReportGenerator CreateGenerator(ReportFormat reportFormat) =>
        _reportGenerators.TryGetValue(reportFormat, out var generator)
            ? generator
            : throw new InvalidOperationException($"Не найден генератор для отчётов формата {reportFormat}");
}