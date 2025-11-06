using QuestPDF.Fluent;
using ReportService.BLL.Common.Templates.QuestPdf.Containers;
using ReportService.DAL.Interfaces.Repositories;

namespace ReportService.BLL.Common.Generators.Pdf;

/// <inheritdoc />
internal class PdfReportGenerator(
    IReportsRepository reportsRepository,
    IReportTemplatesContainer templatesContainer) : IPdfReportGenerator
{
    /// <inheritdoc />
    public async Task<byte[]> GenerateAsync(int templateId, string reportData, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(reportData))
        {
            return [];
        }

        var templateMetadata = await reportsRepository.GetMetadataByIdAsync(templateId, cancellationToken);
        if (templateMetadata is null)
        {
            throw new InvalidOperationException($"Ошибка получения метаданных шаблона с идентификатором {templateId}");
        }

        var template = templatesContainer.GetReportTemplate(templateMetadata.ReportTemplateTypeName);

        var report = Document
            .Create(c => template.Compile(c, reportData))
            .GeneratePdf();

        return report;
    }
}