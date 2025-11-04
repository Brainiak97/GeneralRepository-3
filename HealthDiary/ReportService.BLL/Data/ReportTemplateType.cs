namespace ReportService.BLL.Data;

/// <summary>
/// Тип шаблона отчёта.
/// </summary>
/// <param name="TemplateId">Идентификатор шаблона отчёта.</param>
/// <param name="TemplateName">Имя шаблона.</param>
public record ReportTemplateType(int TemplateId, string TemplateName);