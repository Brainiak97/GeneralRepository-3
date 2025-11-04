namespace ReportService.Api.Contracts.Data.Dto;

/// <summary>
/// Тип шаблона отчёта.
/// </summary>
/// <param name="TemplateId">Идентификатор шаблона отчёта.</param>
/// <param name="TemplateName">Имя шаблона.</param>
public record ReportTemplateTypeDto(int TemplateId, string TemplateName);
