using System.Reflection;
using ReportService.BLL.Reports.Pdf.QuestPdfReports.Interfaces;

namespace ReportService.BLL.Common.Templates.QuestPdf.Containers;

/// <inheritdoc />
internal sealed class ReportTemplatesContainer : IReportTemplatesContainer
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
    public IReportTemplate GetReportTemplate(string templateName) =>
        QuestPdfReportTemplates.TryGetValue(templateName, out var template)
            ? template
            : throw new InvalidOperationException($"Не найден шаблон отчёта с именем {templateName}");
}