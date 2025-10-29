using ReportService.Domain.Models;

namespace ReportService.BLL.Data.Commands;

/// <summary>
/// Команда генерации отчёта.
/// </summary>
/// <param name="EntityId">Идентификатор сущности.</param>
/// <param name="ReportContent">Содержимое отчёта.</param>
/// <param name="TemplateId">Идентификатор шаблона отчёта.</param>
/// <param name="ReportFormat">Формат отчёта.</param>
public record GenerateReportCommand(
    int EntityId,
    string ReportContent,
    int TemplateId,
    ReportFormat ReportFormat);