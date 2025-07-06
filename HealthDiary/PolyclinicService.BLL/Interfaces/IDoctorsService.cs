using PolyclinicService.BLL.Data.Dtos;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Interfaces;

/// <summary>
/// Сервис, предоставляющий методы для работы с врачами, зарегистрированными в приложении.
/// </summary>
public interface IDoctorsService
{
    /// <summary>
    /// Добавить врача в БД.
    /// </summary>
    /// <param name="request">Запрос на добавление врача в БД.</param>
    /// <returns>Идентификатор врача в БД.</returns>
    Task<int> AddAsync(AddDoctorRequest request);
    
    /// <summary>
    /// Обновить данные по врачу.
    /// </summary>
    /// <param name="request">Запрос на редактирование данных по врачу.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task UpdateDoctorInfoAsync(UpdateDoctorRequest request);
    
    /// <summary>
    /// Удалить врача.
    /// </summary>
    /// <param name="doctorId">Идентификатор врача.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task DeleteAsync(int doctorId);
    
    /// <summary>
    /// Вернуть врача по идентификатору.
    /// </summary>
    /// <param name="doctorId">Идентификатор врача.</param>
    /// <returns>Данные по врачу если есть или <see langword="null"/>.</returns>
    Task<DoctorDto?> GetById(int doctorId);
    
    /// <summary>
    /// Вернуть всех врачей.
    /// </summary>
    /// <returns>Коллекция врачей.</returns>
    Task<DoctorDto[]> GetAll();

    /// <summary>
    /// Вернуть всех врачей поликлиники.
    /// </summary>
    /// <param name="polyclinicId">Идентификатор поликлиники.</param>
    /// <returns>Коллекция всех врачей поликлиники.</returns>
    Task<DoctorDto[]> GetPolyclinicDoctors(int polyclinicId);
}