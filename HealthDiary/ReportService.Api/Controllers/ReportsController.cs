using Microsoft.AspNetCore.Authorization;
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
[Authorize]
public class ReportsController(IReportService reportService) : ControllerBase
{
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