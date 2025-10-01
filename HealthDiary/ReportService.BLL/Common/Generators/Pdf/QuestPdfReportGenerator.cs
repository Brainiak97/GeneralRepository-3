using QuestPDF.Fluent;
using ReportService.BLL.Common.Templates.QuestPdf.Containers;
using ReportService.DAL.Interfaces.Repositories;

namespace ReportService.BLL.Common.Generators.Pdf;

/// <inheritdoc />
internal class QuestPdfReportGenerator(
    IReportTemplatesRepository reportTemplatesRepository,
    IReportTemplatesContainer templatesContainer) : IPdfReportGenerator
{
    /// <inheritdoc />
    public async Task<byte[]> GenerateAsync(int templateId, string reportData)
    {
        if (string.IsNullOrEmpty(reportData))
        {
            return [];
        }

        var templateMetadata = await reportTemplatesRepository.GetByIdAsync(templateId);
        if (templateMetadata is null)
        {
            throw new InvalidOperationException($"Ошибка получения метаданных шаблона с идентификатором {templateId}");
        }

        var template = templatesContainer.GetReportTemplate(templateMetadata.ReportTemplateTypeName);

        return Document
            .Create(c => template.Compile(c, reportData))
            .GeneratePdf();
    }
}