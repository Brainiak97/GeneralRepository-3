using Microsoft.AspNetCore.Mvc;
using ReportService.Api.Contracts.Enums;
using ReportService.Api.WebRoutes;
using ReportService.BLL.Interfaces;

namespace ReportService.Api.Controllers;

/// <summary>
/// Предоставляет API-методы для работы с отчётами.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReportsController(IReportService reportService) : ControllerBase
{
    /// <summary>
    /// Вернуть отчёт по идентификатору результата приёма врача.
    /// </summary>
    /// <param name="id">Идентификатор результата приёма врача.</param>
    /// <param name="reportFormat">Формат отчёта.</param>
    /// <returns>Отчёт.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetReportByAppointmentResultId(int id, ReportFormat reportFormat)
    {
        var response = await reportService.GenerateReportAsync(id, reportFormat);
        return response?.ReportContent is not { Length: > 0 }
            ? BadRequest()
            : File(response.ReportContent, GetContentTypeByFormat(reportFormat) ,response.FileName);
    }

    /// <summary>
    /// Вернуть все шаблоны отчётов зарегистрированные в сервисе.
    /// </summary>
    /// <returns></returns>
    [HttpGet(ReportsControllerWebRoutes.GetReportTemplateTypes)]
    public Task<IActionResult> GetReportTemplateTypes()
    {
        throw new NotImplementedException();
    }

    private static string GetContentTypeByFormat(ReportFormat reportFormat) =>
        reportFormat switch
        {
            ReportFormat.Pdf => "application/pdf",
            _ => throw new ArgumentOutOfRangeException(nameof(reportFormat), reportFormat, $"Content Type не найден для отчёта с типом {reportFormat}")
        };
}