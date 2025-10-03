namespace ReportService.Domain.Dtos;

/// <summary>
/// Результат приёма пациента.
/// </summary>
/// <param name="Id">Идентификатор приёма.</param>
/// <param name="ReportTemplateId">Идентификатор типа шаблона.</param>
/// <param name="ReportContent">Содержание отчёта.</param>
public record AppointmentResultDto(int Id, int ReportTemplateId, string ReportContent);