using PolyclinicService.BLL.Data.Dtos;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Interfaces;

/// <summary>
/// Сервис, предоставляющий методы для работы с результатами приёмов пациентов.
/// </summary>
public interface IAppointmentResultsService
{
    /// <summary>
    /// Сохранить данные по результату приёма пациента.
    /// </summary>
    /// <param name="request">Запрос на сохранение результата приёма.</param>
    /// <returns>Идентификатор результата приёма.</returns>
    Task<int> SaveAppointmentResultAsync(SaveAppointmentResultRequest request);

    /// <summary>
    /// Изменить данные по результату приёма пациента.
    /// </summary>
    /// <param name="request">Запрос на редактирование результата приёма.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task UpdateAppointmentResultAsync(UpdateAppointmentResultRequest request);

    /// <summary>
    /// Удалить данные по результату приёма пациента.
    /// </summary>
    /// <param name="id">Идентификатор результата приёма.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task DeleteAppointmentResultAsync(int id);

    /// <summary>
    /// Вернуть данные по результату приёма пациента.
    /// </summary>
    /// <param name="id">Идентификатор результата приёма.</param>
    /// <returns>Результат приёма пациента или <see langword="null"/> если нет.</returns>
    Task<AppointmentResultDto?> GetAppointmentResultByIdAsync(int id);
}