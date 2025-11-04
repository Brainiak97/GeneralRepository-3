using ReportService.BLL.Common.Generators.Pdf;
using ReportService.Domain.Models;

namespace ReportService.BLL.Common.Generators.Factories;

/// <inheritdoc />
internal class ReportGeneratorFactory(Func<IPdfReportGenerator> pdfReportGenerator) : IReportGeneratorFactory
{
    /// <inheritdoc />
    public IReportGenerator CreateGenerator(ReportFormat reportFormat) =>
        reportFormat switch
        {
            ReportFormat.Pdf => pdfReportGenerator(),
            _ => throw new InvalidOperationException($"Не удалось создать генератор для отчётов формата {reportFormat}")
        };
}