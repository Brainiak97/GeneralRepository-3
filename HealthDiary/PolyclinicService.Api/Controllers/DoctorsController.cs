using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolyclinicService.Api.WebRoutes;
using PolyclinicService.BLL.Data.Requests;
using PolyclinicService.BLL.Interfaces;

namespace PolyclinicService.Api.Controllers;

/// <summary>
/// Предоставляет API методы для работы с врачами.
/// </summary>
/// <param name="doctorsService">Сервис, предоставляющий методы для работы с врачами, зарегистрированными в приложении.</param>
[ApiController]
[Route(DoctorsControllerWebRoutes.BasePath)]
public class DoctorsController(IDoctorsService doctorsService) : ControllerBase
{
    /// <summary>
    /// Вернуть данные по врачу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор врача.</param>
    /// <returns>Данные по врачу.</returns>
    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetDoctorById([FromRoute] int id)
    {
        var result = await doctorsService.GetById(id);
        return result is null
            ? NotFound()
            : Ok(result);
    }

    /// <summary>
    /// Вернуть всех врачей, зарегистрированных в приложении.
    /// </summary>
    /// <returns>Все врачи, зарегистрированные в приложении.</returns>
    [HttpGet(DoctorsControllerWebRoutes.GetDoctorsRoute)]
    [Authorize]
    public async Task<IActionResult> GetDoctors()
    {
        var result = await doctorsService.GetAll();
        return Ok(result);
    }

    /// <summary>
    /// Вернуть всех врачей поликлиники.
    /// </summary>
    /// <param name="polyclinicId">Идентификатор поликлиники.</param>
    /// <returns>Все врачи поликлиники.</returns>
    [HttpGet(DoctorsControllerWebRoutes.GetPolyclinicDoctorsRoute)]
    [Authorize]
    public async Task<IActionResult> GetPolyclinicDoctors([FromRoute] int polyclinicId)
    {
        var result = await doctorsService.GetPolyclinicDoctors(polyclinicId);
        return result.Length is 0
            ? NotFound()
            : Ok(result);
    }

    /// <summary>
    /// Добавить врача.
    /// </summary>
    /// <param name="request">Запрос на добавление врача в БД.</param>
    /// <returns>Идентификатор врача в приложении.</returns>
    [HttpPost(DoctorsControllerWebRoutes.AddDoctorRoute)]
    [Authorize]
    public async Task<IActionResult> AddDoctor([FromBody] AddDoctorRequest request)
    {
        var result = await doctorsService.AddAsync(request);
        return result > 0
            ? Ok(result)
            : StatusCode((int)HttpStatusCode.InternalServerError);
    }

    /// <summary>
    /// Изменить данные по врачу.
    /// </summary>
    /// <param name="request">Запрос на редактирование данных по врачу.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpPut(DoctorsControllerWebRoutes.UpdateDoctorInfoRoute)]
    [Authorize]
    public async Task<IActionResult> UpdateDoctorInfo([FromBody] UpdateDoctorRequest request)
    {
        await doctorsService.UpdateDoctorInfoAsync(request);
        return Ok();
    }

    /// <summary>
    /// Удалить врача.
    /// </summary>
    /// <param name="id">Идентификатор врача.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpDelete(DoctorsControllerWebRoutes.DeleteDoctorRoute)]
    [Authorize]
    public async Task<IActionResult> DeleteDoctor([FromRoute] int id)
    {
        await doctorsService.DeleteAsync(id);
        return Ok();
    }
}