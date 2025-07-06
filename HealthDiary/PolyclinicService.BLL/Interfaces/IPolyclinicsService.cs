using PolyclinicService.BLL.Data.Dtos;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Interfaces;

/// <summary>
/// Сервис, предоставляющий методы для работы с поликлиниками.
/// </summary>
public interface IPolyclinicsService
{
    /// <summary>
    /// Добавить поликлинику.
    /// </summary>
    /// <param name="request">Запрос на добавление поликлиники.</param>
    /// <returns>Идентификатор добавленной поликлиники в базу данных.</returns>
    Task<int> AddPolyclinicAsync(AddPolyclinicRequest request);
    
    /// <summary>
    /// Обновить данные по поликлинике.
    /// </summary>
    /// <param name="request">Запрос на редактирование данных о поликлинике.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task UpdatePolyclinicInfoAsync(UpdatePolyclinicRequest request);
    
    /// <summary>
    /// Удалить поликлинику.
    /// </summary>
    /// <param name="polyclinicId"></param>
    /// <returns><see cref="Task"/>.</returns>
    Task DeletePolyclinicAsync(int polyclinicId);
    
    /// <summary>
    /// Вернуть информацию о поликлинике по идентификатору.
    /// </summary>
    /// <param name="polyclinicId">Идентификатор поликлиники.</param>
    /// <returns>Данные о поликлинике по идентификатору.</returns>
    Task<PolyclinicDto?> GetPolyclinicById(int polyclinicId);

    /// <summary>
    /// Вернуть все поликлиники.
    /// </summary>
    /// <returns>Коллекцию со всеми зарегистрированными поликлиниками.</returns>
    Task<PolyclinicDto[]> GetAllPolyclinics();
}