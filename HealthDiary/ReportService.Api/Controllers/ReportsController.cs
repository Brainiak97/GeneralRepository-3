using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportService.Api.Contracts.Enums;
using ReportService.Api.WebRoutes;

namespace ReportService.Api.Controllers;

/// <summary>
/// Предоставляет API-методы для работы с отчётами.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportsController : ControllerBase
{
    /// <summary>
    /// Вернуть отчёт по идентификатору результата приёма врача.
    /// </summary>
    /// <param name="appointmentResultId">Идентификатор результата приёма врача.</param>
    /// <param name="reportFormat">Формат отчёта.</param>
    /// <returns>Отчёт.</returns>
    [HttpGet("{appointmentResultId:int}")]
    public Task<IActionResult> GetReportByAppointmentResultId(int appointmentResultId, ReportFormat reportFormat)
    {
        throw new NotImplementedException();
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
}