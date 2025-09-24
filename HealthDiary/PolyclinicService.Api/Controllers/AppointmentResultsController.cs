using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolyclinicService.Api.WebRoutes;
using PolyclinicService.BLL.Data.Requests;
using PolyclinicService.BLL.Interfaces;

namespace PolyclinicService.Api.Controllers;

/// <summary>
/// Предоставляет API методы для работы с данными результатов приёмов пациентов.
/// </summary>
/// <param name="appointmentResultsService">Сервис, предоставляющий методы для работы с результатами приёмов пациентов.</param>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentResultsController(IAppointmentResultsService appointmentResultsService) : ControllerBase
{
    /// <summary>
    /// Сохранить данные по результату приёма пациента.
    /// </summary>
    /// <param name="request">Запрос на сохранение результата приёма.</param>
    /// <returns>Идентификатор результата приёма.</returns>
    [HttpPost(AppointmentResultWebRoutes.SaveAppointmentResult)]
    public async Task<IActionResult> SaveAppointmentResult(SaveAppointmentResultRequest request)
    {
        var result = await appointmentResultsService.SaveAppointmentResultAsync(request);
        return result > 0
            ? Ok(result)
            : StatusCode((int)HttpStatusCode.InternalServerError);
    }

    /// <summary>
    /// Изменить данные по результату приёма пациента.
    /// </summary>
    /// <param name="request">Запрос на редактирование результата приёма.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpPut(AppointmentResultWebRoutes.UpdateAppointmentResult)]
    public async Task<IActionResult> UpdateAppointmentResult(UpdateAppointmentResultRequest request)
    {
        await appointmentResultsService.UpdateAppointmentResultAsync(request);
        return Ok();
    }

    /// <summary>
    /// Удалить данные по результату приёма пациента.
    /// </summary>
    /// <param name="id">Идентификатор результата приёма.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpDelete(AppointmentResultWebRoutes.DeleteAppointmentResult)]
    public async Task<IActionResult> DeleteAppointmentResult([FromRoute] int id)
    {
        await appointmentResultsService.DeleteAppointmentResultAsync(id);
        return Ok();
    }

    /// <summary>
    /// Вернуть данные по результату приёма пациента.
    /// </summary>
    /// <param name="id">Идентификатор результата приёма.</param>
    /// <returns>Результат приёма пациента.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAppointmentResult([FromRoute] int id)
    {
        var result = await appointmentResultsService.GetAppointmentResultByIdAsync(id);
        return result is null
            ? NotFound()
            : Ok(result);
    }

    /// <summary>
    /// Вернуть результаты приёмов пациента.
    /// </summary>
    /// <param name="patientId">Идентификатор пациента.</param>
    /// <param name="date">Дата, на которую необходимо получить результаты приёма.</param>
    /// <returns>Результаты приёма пациента с данными слотов из графика поликлиники.</returns>
    [HttpGet(AppointmentResultWebRoutes.GetPatientAppointments)]
    public async Task<IActionResult> GetPatientAppointments([FromRoute] int patientId, [FromQuery] DateTime? date)
    {
        var results = await appointmentResultsService.GetPatientAppointmentResultsWithSlotInfoAsync(
            patientId,
            date);

        return Ok(results);
    }
}