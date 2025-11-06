using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolyclinicService.Api.WebRoutes;
using PolyclinicService.BLL.Data.Requests;
using PolyclinicService.BLL.Interfaces;

namespace PolyclinicService.Api.Controllers;

/// <summary>
/// Предоставляет API методы для работы с графиками приёмов поликлиник.
/// </summary>
/// <param name="polyclinicSchedulesService">Сервис, предоставляющий методы для работы с графиками приёма поликлиники.</param>
[ApiController]
[Route("api/[controller]")]
public class PolyclinicSchedulesController(IPolyclinicSchedulesService polyclinicSchedulesService) : ControllerBase
{
    /// <summary>
    /// Добавить слот приёма к врачу в график поликлиники.
    /// </summary>
    /// <param name="request">Запрос на добавление слота приёма к врачу в график поликлиники.</param>
    /// <returns>Идентификатор добавленного слота в графике поликлиники.</returns>
    [HttpPost(PolyclinicSchedulesControllerWebRoutes.AddAppointmentSlotRoute)]
    [Authorize]
    public async Task<IActionResult> AddAppointmentSlot([FromBody] AddAppoinmentSlotRequest request)
    {
        var result = await polyclinicSchedulesService.AddAppointmentSlotAsync(request);
        return result > 0 ? Ok(result) : StatusCode((int)HttpStatusCode.InternalServerError);
    }

    /// <summary>
    /// Добавить слоты приёмов к врачу в график по шаблону.
    /// </summary>
    /// <param name="request">Запрос на добавление слотов приёмов к врачу по шаблону.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpPost(PolyclinicSchedulesControllerWebRoutes.AddAppointmentSlotsByTemplateRoute)]
    [Authorize]
    public async Task<IActionResult> AddAppointmentSlotsByTemplate([FromBody] AddAppointmentSlotsByTemplateRequest request)
    {
        var isCorrect = await polyclinicSchedulesService.AddAppointmentSlotsByTemplate(request);
        return isCorrect ? Ok(isCorrect) : StatusCode((int)HttpStatusCode.InternalServerError);
    }

    /// <summary>
    /// Редактировать данные о слоте приёма к врачу в графике.
    /// </summary>
    /// <param name="request">Запрос на редактирование данных слота приёма к врачу.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpPut(PolyclinicSchedulesControllerWebRoutes.UpdateAppointmentSlotRoute)]
    [Authorize]
    public async Task<IActionResult> UpdateAppointmentSlot([FromBody] UpdateAppointmentSlotRequest request)
    {
        await polyclinicSchedulesService.UpdateAppointmentSlotAsync(request);
        return Ok();
    }

    /// <summary>
    /// Редактировать статус слота приёма к врачу в графике.
    /// </summary>
    /// <param name="request">Запрос на редактирование статуса приёма к врачу в графике поликлиники.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpPut(PolyclinicSchedulesControllerWebRoutes.UpdateAppointmentSlotStatusRoute)]
    [Authorize]
    public async Task<IActionResult> UpdateAppointmentSlotStatus([FromBody] UpdateAppointmentSlotStatusRequest request)
    {
        await polyclinicSchedulesService.UpdateAppointmentSlotStatusAsync(request);
        return Ok();
    }

    /// <summary>
    /// Удалить слот приёма к врачу.
    /// </summary>
    /// <param name="id">Идентификатор слота приёма в графике.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpDelete(PolyclinicSchedulesControllerWebRoutes.DeleteAppointmentSlotRoute)]
    [Authorize]
    public async Task<IActionResult> DeleteAppointmentSlot([FromRoute] int id)
    {
        await polyclinicSchedulesService.DeleteAppointmentSlotAsync(id);
        return Ok();
    }

    /// <summary>
    /// Удалить слоты приёма к врачу в графике по фильтру.
    /// </summary>
    /// <param name="request">Запрос на удаление слотов на приёмы к врачу поликлиники по фильтру.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpDelete(PolyclinicSchedulesControllerWebRoutes.DeletePolyclinicAppointmentSlotsByFilterRoute)]
    [Authorize]
    public async Task<IActionResult> DeletePolyclinicAppointmentSlotsByFilter(
        [FromBody] DeletePolyclinicAppointmentSlotsByFilterRequest request)
    {
        await polyclinicSchedulesService.DeletePolyclinicAppointmentSlotsByFilterAsync(request);
        return Ok();
    }

    /// <summary>
    /// Вернуть слот приёма к врачу из графика.
    /// </summary>
    /// <param name="id">Идентификатор слота приёма в графике.</param>
    /// <returns>Слот приёма к врачу по идентификатору.</returns>
    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetAppointmentSlotById([FromRoute] int id)
    {
        var result = await polyclinicSchedulesService.GetAppointmentSlotByIdAsync(id);
        return result is not null
            ? Ok(result)
            : NotFound();
    }

    /// <summary>
    /// Вернуть все слоты приёмов ко всем врачам поликлиники на дату.
    /// </summary>
    /// <param name="request">Запрос на получение всех слотов приёмов к врачам поликлиники на дату.</param>
    /// <returns>Слоты приёмов ко всем врачам поликлиники на дату.</returns>
    [HttpPost(PolyclinicSchedulesControllerWebRoutes.GetPolyclinicAppointmentSlotsByDateRoute)]
    [Authorize]
    public async Task<IActionResult> GetPolyclinicAppointmentSlotsByDateAsync([FromBody] PolyclinicAppointmentSlotsByDateRequest request)
    {
        var result = await polyclinicSchedulesService.GetPolyclinicAppointmentSlotsByDateAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Вернуть все активные слоты приёма к врачу.
    /// </summary>
    /// <param name="request">Запрос на получение активных слотов приёмов к врачу.</param>
    /// <returns>Активные слоты приёмов к врачу.</returns>
    [HttpPost(PolyclinicSchedulesControllerWebRoutes.GetDoctorActiveAppointmentSlotsRoute)]
    [Authorize]
    public async Task<IActionResult> GetDoctorActiveAppointmentSlotsAsync([FromBody] DoctorActiveAppointmentSlotsRequest request)
    {
        var result = await polyclinicSchedulesService.GetDoctorActiveAppointmentSlotsAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Резервация слота пользователем с подтверждением доступа к личным метрикам.
    /// </summary>
    /// <param name="request">Запрос на получение активных слотов приёмов к врачу.</param>
    /// <returns>Активные слоты приёмов к врачу.</returns>
    [HttpPost(PolyclinicSchedulesControllerWebRoutes.SlotReservationAsync)]
    [Authorize]
    public async Task<IActionResult> SlotReservationAsync([FromBody] UserSlotReservationRequest request)
    {
        await polyclinicSchedulesService.SlotReservationAsync(request);
        return Ok();
    }
}