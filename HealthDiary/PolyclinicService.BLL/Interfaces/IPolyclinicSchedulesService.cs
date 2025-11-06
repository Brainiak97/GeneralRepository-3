using PolyclinicService.BLL.Data.Dtos;
using PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.BLL.Interfaces;

/// <summary>
/// Сервис, предоставляющий методы для работы с графиками приёма поликлиники.
/// </summary>
public interface IPolyclinicSchedulesService
{
    /// <summary>
    /// Добавить слот приёма к врачу в график поликлиники.
    /// </summary>
    /// <param name="request">Запрос на добавление слота приёма к врачу в график поликлиники.</param>
    /// <returns>Идентификатор добавленного слота в графике поликлиники.</returns>
    Task<int> AddAppointmentSlotAsync(AddAppoinmentSlotRequest request);

    /// <summary>
    /// Добавить слоты приёмов к врачу в график по шаблону.
    /// </summary>
    /// <param name="request">Запрос на добавление слотов приёмов к врачу по шаблону.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task<bool> AddAppointmentSlotsByTemplate(AddAppointmentSlotsByTemplateRequest request);

    /// <summary>
    /// Редактировать данные о слоте приёма к врачу в графике.
    /// </summary>
    /// <param name="request">Запрос на редактирование данных слота приёма к врачу.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task UpdateAppointmentSlotAsync(UpdateAppointmentSlotRequest request);

    /// <summary>
    /// Редактировать статус слота приёма к врачу в графике.
    /// </summary>
    /// <param name="request">Запрос на редактирование статуса приёма к врачу в графике поликлиники.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task UpdateAppointmentSlotStatusAsync(UpdateAppointmentSlotStatusRequest request);

    /// <summary>
    /// Удалить слот приёма к врачу.
    /// </summary>
    /// <param name="id">Идентификатор слота приёма в графике.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task DeleteAppointmentSlotAsync(int id);

    /// <summary>
    /// Удалить слоты приёма к врачу в графике по фильтру.
    /// </summary>
    /// <param name="request">Запрос на удаление слотов на приёмы к врачу поликлиники по фильтру.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task DeletePolyclinicAppointmentSlotsByFilterAsync(DeletePolyclinicAppointmentSlotsByFilterRequest request);

    /// <summary>
    /// Вернуть слот приёма к врачу из графика.
    /// </summary>
    /// <param name="id">Идентификатор слота приёма в графике.</param>
    /// <returns>Слот приёма к врачу по идентификатору или <see langword="null"/> если нет.</returns>
    Task<AppointmentSlotDto?> GetAppointmentSlotByIdAsync(int id);

    /// <summary>
    /// Вернуть все слоты приёмов ко всем врачам поликлиники на дату.
    /// </summary>
    /// <param name="request">Запрос на получение всех слотов приёмов к врачам поликлиники на дату.</param>
    /// <returns>Слоты приёмов ко всем врачам поликлиники на дату.</returns>
    Task<AppointmentSlotDto[]> GetPolyclinicAppointmentSlotsByDateAsync(PolyclinicAppointmentSlotsByDateRequest request);

    /// <summary>
    /// Вернуть все активные слоты приёма к врачу.
    /// </summary>
    /// <param name="request">Запрос на получение активных слотов приёмов к врачу.</param>
    /// <returns>Активные слоты приёмов к врачу.</returns>
    Task<AppointmentSlotDto[]> GetDoctorActiveAppointmentSlotsAsync(DoctorActiveAppointmentSlotsRequest request);

    /// <summary>
    /// Резервация слота пользователем с подтверждением доступа к личным метрикам.
    /// </summary>
    /// <param name="request">Запрос для резервации слота приема.</param>
    /// <returns></returns>
    Task SlotReservationAsync(UserSlotReservationRequest request);
}