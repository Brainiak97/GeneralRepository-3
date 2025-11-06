using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReportService.Api.Contracts.Data.Dto;
using ReportService.BLL.Interfaces;
using ReportService.Common.Helpers;

namespace ReportService.Api.Controllers;

/// <summary>
/// Предоставляет API-методы для работы с отчётами.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReportsController(
    IReportService reportService,
    IMapper mapper)
    : ControllerBase
{
    /// <summary>
    /// Вернуть все типы шаблонов отчётов зарегистрированные в сервисе.
    /// </summary>
    /// <returns>Шаблоны отчётов зарегистированные в сервисе.</returns>
    [HttpGet(nameof(GetReportTemplateTypes))]
    public async Task<IActionResult> GetReportTemplateTypes()
    {
        var reportTemplateTypes = await reportService.GetReportTemplateTypesAsync();
        var result = reportTemplateTypes?.Select(mapper.Map<ReportTemplateTypeDto>) ?? [];
        return Ok(result);
    }

    /// <summary>
    /// Скачать отчёт.
    /// </summary>
    /// <param name="reportId">Идентификатор отчёта.</param>
    /// <returns>Отчёт</returns>
    [HttpGet(nameof(DownloadReport))]
    public async Task<IActionResult> DownloadReport(int reportId)
    {
        var report = await reportService.GetReportByIdAsync(reportId);
        if (report is null)
        {
            return NotFound();
        }

        var contentType = ReportServiceHelper.GetContentTypeByFormat(report.ReportFormat);
        return File(report.Content, contentType, report.FileName);
    }

    /// <summary>
    /// Вернуть шаблон отчёта по идентификатору.
    /// </summary>
    /// <param name="templateId">Идентификатор шаблона отчёта.</param>
    /// <returns>Шаблон отчёта.</returns>
    [HttpGet(nameof(GetReportTemplateById))]
    public async Task<IActionResult> GetReportTemplateById(int templateId)
    {
        var templateFields = await reportService.GetReportTemplateByIdAsync(templateId);
        if (templateFields is not { Count: > 0 })
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(templateFields.Select(mapper.Map<TemplateFieldDto>));
    }
}