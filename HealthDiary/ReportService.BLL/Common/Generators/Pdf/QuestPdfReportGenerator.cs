using System.Reflection;
using QuestPDF.Fluent;
using ReportService.BLL.Reports.Pdf.QuestPdfReports.Interfaces;
using ReportService.DAL.Interfaces.Repositories;

namespace ReportService.BLL.Common.Generators.Pdf;

/// <inheritdoc />
internal class QuestPdfReportGenerator(IReportTemplatesRepository reportTemplatesRepository) : IPdfReportGenerator
{
    private static readonly Dictionary<string, IReportTemplate> QuestPdfReportTemplates =
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => !t.IsAbstract && typeof(IReportTemplate).IsAssignableFrom(t))
            .Select<Type, KeyValuePair<string, IReportTemplate>>(type =>
            {
                if (Activator.CreateInstance(type) is not IReportTemplate instance)
                {
                    throw new InvalidOperationException($"Ошибка создания шаблона отчёта с типом {type.Name}");
                }

                return new KeyValuePair<string, IReportTemplate>(type.Name, instance);
            })
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

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
            throw new InvalidOperationException($"Ошибка получения шаблона с идентификатором {templateId}");
        }

        var template = QuestPdfReportTemplates.TryGetValue(templateMetadata.ReportTemplateTypeName, out var reportTemplate)
            ? reportTemplate
            : throw new InvalidOperationException($"По типу {templateMetadata.ReportTemplateTypeName} не удалось сформировать шаблон отчёта");

        return Document
            .Create(c => template.Compile(c, reportData))
            .GeneratePdf();
    }
}