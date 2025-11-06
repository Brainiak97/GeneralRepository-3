using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolyclinicService.Api.Contracts.Data.Requests;
using PolyclinicService.BLL.Data.Commands;
using PolyclinicService.BLL.Interfaces;

namespace PolyclinicService.Api.Controllers;

/// <summary>
/// Предоставляет API методы для работы с графиками приёмов поликлиник.
/// </summary>
/// <param name="polyclinicSchedulesService">Сервис, предоставляющий методы для работы с графиками приёма поликлиники.</param>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PolyclinicSchedulesController(
    IPolyclinicSchedulesService polyclinicSchedulesService,
    IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Добавить слот приёма к врачу в график поликлиники.
    /// </summary>
    /// <param name="request">Запрос на добавление слота приёма к врачу в график поликлиники.</param>
    /// <returns>Идентификатор добавленного слота в графике поликлиники.</returns>
    [HttpPost(nameof(AddAppointmentSlot))]
    public async Task<IActionResult> AddAppointmentSlot([FromBody] AddAppoinmentSlotRequest request)
    {
        var command = mapper.Map<AddAppoinmentSlotCommand>(request);
        var result = await polyclinicSchedulesService.AddAppointmentSlotAsync(command);
        return result > 0 ? Ok(result) : StatusCode((int)HttpStatusCode.InternalServerError);
    }

    /// <summary>
    /// Добавить слоты приёмов к врачу в график по шаблону.
    /// </summary>
    /// <param name="request">Запрос на добавление слотов приёмов к врачу по шаблону.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpPost(nameof(AddAppointmentSlotsByTemplate))]
    public async Task<IActionResult> AddAppointmentSlotsByTemplate([FromBody] AddAppointmentSlotsByTemplateRequest request)
    {
        var command = mapper.Map<AddAppointmentSlotsByTemplateCommand>(request);
        var isCorrect = await polyclinicSchedulesService.AddAppointmentSlotsByTemplate(command);
        return isCorrect ? Ok(isCorrect) : StatusCode((int)HttpStatusCode.InternalServerError);
    }

    /// <summary>
    /// Редактировать данные о слоте приёма к врачу в графике.
    /// </summary>
    /// <param name="request">Запрос на редактирование данных слота приёма к врачу.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpPut(nameof(UpdateAppointmentSlot))]
    public async Task<IActionResult> UpdateAppointmentSlot([FromBody] UpdateAppointmentSlotRequest request)
    {
        var command = mapper.Map<UpdateAppointmentSlotCommand>(request);
        await polyclinicSchedulesService.UpdateAppointmentSlotAsync(command);
        return Ok();
    }

    /// <summary>
    /// Редактировать статус слота приёма к врачу в графике.
    /// </summary>
    /// <param name="request">Запрос на редактирование статуса приёма к врачу в графике поликлиники.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpPut(nameof(UpdateAppointmentSlotStatus))]
    public async Task<IActionResult> UpdateAppointmentSlotStatus([FromBody] UpdateAppointmentSlotStatusRequest request)
    {
        var command = mapper.Map<UpdateAppointmentSlotStatusCommand>(request);
        await polyclinicSchedulesService.UpdateAppointmentSlotStatusAsync(command);
        return Ok();
    }

    /// <summary>
    /// Удалить слот приёма к врачу.
    /// </summary>
    /// <param name="id">Идентификатор слота приёма в графике.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpDelete(nameof(DeleteAppointmentSlot))]
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
    [HttpDelete(nameof(DeletePolyclinicAppointmentSlotsByFilter))]
    public async Task<IActionResult> DeletePolyclinicAppointmentSlotsByFilter(
        [FromBody] DeletePolyclinicAppointmentSlotsByFilterRequest request)
    {
        var command = mapper.Map<DeletePolyclinicAppointmentSlotsByFilterCommand>(request);
        await polyclinicSchedulesService.DeletePolyclinicAppointmentSlotsByFilterAsync(command);
        return Ok();
    }

    /// <summary>
    /// Вернуть слот приёма к врачу из графика.
    /// </summary>
    /// <param name="id">Идентификатор слота приёма в графике.</param>
    /// <returns>Слот приёма к врачу по идентификатору.</returns>
    [HttpGet(nameof(GetAppointmentSlotById))]
    public async Task<IActionResult> GetAppointmentSlotById([FromQuery] int id)
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
    [HttpPost(nameof(GetPolyclinicAppointmentSlotsByDate))]
    public async Task<IActionResult> GetPolyclinicAppointmentSlotsByDate([FromBody] PolyclinicAppointmentSlotsByDateRequest request)
    {
        var command = mapper.Map<PolyclinicAppointmentSlotsByDateCommand>(request);
        var result = await polyclinicSchedulesService.GetPolyclinicAppointmentSlotsByDateAsync(command);
        return Ok(result);
    }

    /// <summary>
    /// Вернуть все активные слоты приёма к врачу.
    /// </summary>
    /// <param name="request">Запрос на получение активных слотов приёмов к врачу.</param>
    /// <returns>Активные слоты приёмов к врачу.</returns>
    [HttpPost(nameof(GetDoctorActiveAppointmentSlots))]
    public async Task<IActionResult> GetDoctorActiveAppointmentSlots([FromBody] DoctorActiveAppointmentSlotsRequest request)
    {
        var command = mapper.Map<DoctorActiveAppointmentSlotsCommand>(request);
        var result = await polyclinicSchedulesService.GetDoctorActiveAppointmentSlotsAsync(command);
        return Ok(result);
    }

    /// <summary>
    /// Вернуть все слоты приёма пациента.
    /// </summary>
    /// <param name="patientId">Идентификатор пациента (пользователя в системе).</param>
    /// <param name="startDate">Дата с которой необходимо получить слоты (опционально).</param>
    /// <param name="endDate">Дата по которую необходимо получить слоты (опционально).</param>
    /// <returns>Слоты приёма пациента.</returns>
    [HttpGet(nameof(GetPatientAppointmentSlots))]
    public async Task<IActionResult> GetPatientAppointmentSlots(
        [FromQuery] int patientId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        var result = await polyclinicSchedulesService.GetPatientAppointmentSlotsAsync(
            new GetPatientAppointmentSlotsCommand(patientId, startDate, endDate));
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