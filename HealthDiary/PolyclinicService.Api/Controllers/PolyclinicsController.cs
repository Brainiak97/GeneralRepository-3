using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolyclinicService.Api.WebRoutes;
using PolyclinicService.BLL.Data.Requests;
using PolyclinicService.BLL.Interfaces;

namespace PolyclinicService.Api.Controllers;

/// <summary>
/// Предоставляет API методы для работы с поликлиниками.
/// </summary>
/// <param name="polyclinicsService">Сервис, предоставляющий методы для работы с поликлиниками.</param>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PolyclinicsController(IPolyclinicsService polyclinicsService) : ControllerBase
{
    /// <summary>
    /// Вернуть информацию о поликлинике по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор поликлиники.</param>
    /// <returns>Данные о поликлинике по идентификатору.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPolyclinicById([FromRoute] int id)
    {
        var result = await polyclinicsService.GetPolyclinicById(id);
        return result is null
            ? NotFound()
            : Ok(result);
    }

    /// <summary>
    /// Вернуть все поликлиники.
    /// </summary>
    /// <returns>Все поликлиники в сервисе.</returns>
    [HttpGet(PolyclinicsControllerWebRoutes.GetPolyclinicsRoute)]
    public async Task<IActionResult> GetPolyclinics()
    {
        var result = await polyclinicsService.GetAllPolyclinics();
        return Ok(result);
    }

    /// <summary>
    /// Добавить поликлинику в сервис.
    /// </summary>
    /// <param name="request">Запрос на добавление поликлиники.</param>
    /// <returns>Идентификатор поликлиники.</returns>
    [HttpPost(PolyclinicsControllerWebRoutes.AddPolyclinicRoute)]
    public async Task<IActionResult> AddPolyclinic([FromBody] AddPolyclinicRequest request)
    {
        var result = await polyclinicsService.AddPolyclinicAsync(request);
        return result > 0
            ? Ok(result)
            : StatusCode((int)HttpStatusCode.InternalServerError);
    }

    /// <summary>
    /// Редактировать данные по поликлинике.
    /// </summary>
    /// <param name="request">Запрос на редактирование данных о поликлинике.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpPut(PolyclinicsControllerWebRoutes.UpdatePolyclinicInfoRoute)]
    public async Task<IActionResult> UpdatePolyclinicInfo([FromBody] UpdatePolyclinicRequest request)
    {
        await polyclinicsService.UpdatePolyclinicInfoAsync(request);
        return Ok();
    }

    /// <summary>
    /// Удалить поликлинику.
    /// </summary>
    /// <param name="id">Идентификатор поликлиники.</param>
    /// <returns><see cref="IActionResult"/>.</returns>
    [HttpDelete(PolyclinicsControllerWebRoutes.DeletePolyclinicRoute)]
    public async Task<IActionResult> DeletePolyclinic([FromRoute] int id)
    {
        await polyclinicsService.DeletePolyclinicAsync(id);
        return Ok();
    }
}