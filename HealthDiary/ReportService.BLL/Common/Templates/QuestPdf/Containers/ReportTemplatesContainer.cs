using System.Reflection;
using ReportService.BLL.Reports.Pdf.QuestPdfReports.Interfaces;

namespace ReportService.BLL.Common.Templates.QuestPdf.Containers;

/// <inheritdoc />
internal sealed class ReportTemplatesContainer : IReportTemplatesContainer
{
    private readonly IReadOnlyDictionary<string, IReportTemplate> _questPdfReportTemplates;

    public ReportTemplatesContainer()
    {
        _questPdfReportTemplates = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => !t.IsAbstract && typeof(IReportTemplate).IsAssignableFrom(t))
            .Select<Type, (string Key, IReportTemplate Value)>(type =>
            {
                if (Activator.CreateInstance(type) is not IReportTemplate instance)
                {
                    throw new InvalidOperationException($"Ошибка создания шаблона отчёта с типом {type.Name}");
                }

                return new (type.Name, instance);
            })
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);    
    }

    /// <inheritdoc />
    public IReportTemplate GetReportTemplate(string templateName) =>
        _questPdfReportTemplates.TryGetValue(templateName, out var template)
            ? template
            : throw new InvalidOperationException($"Не найден шаблон отчёта с именем {templateName}");
}