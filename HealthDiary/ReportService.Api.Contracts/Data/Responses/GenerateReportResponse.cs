namespace ReportService.Api.Contracts.Data.Responses;

/// <summary>
/// Ответ на запрос генерации отчёта.
/// </summary>
/// <param name="ReportContent">Отчёт.</param>
/// <param name="FileName">Имя файла.</param>
public record GenerateReportResponse(byte[] ReportContent, string FileName);