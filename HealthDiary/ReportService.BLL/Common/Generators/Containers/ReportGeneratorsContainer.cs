using ReportService.Api.Contracts.Enums;
using ReportService.BLL.Common.Generators.Pdf;

namespace ReportService.BLL.Common.Generators.Containers;

/// <inheritdoc />
internal class ReportGeneratorsContainer(IPdfReportGenerator pdfReportGenerator) : IReportGeneratorsContainer
{
    /// <inheritdoc />
    public IReportGenerator GetGenerator(ReportFormat reportFormat) =>
        reportFormat switch
        {
            ReportFormat.Pdf => pdfReportGenerator,
            _ => throw new InvalidOperationException($"Не найден генератор для отчётов формата {reportFormat}")
        };
}