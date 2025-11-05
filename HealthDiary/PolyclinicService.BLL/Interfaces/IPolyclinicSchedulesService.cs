using PolyclinicService.BLL.Data.Commands;
using PolyclinicService.BLL.Data.Dtos;

namespace PolyclinicService.BLL.Interfaces;

/// <summary>
/// Сервис, предоставляющий методы для работы с графиками приёма поликлиники.
/// </summary>
public interface IPolyclinicSchedulesService
{
    /// <summary>
    /// Добавить слот приёма к врачу в график поликлиники.
    /// </summary>
    /// <param name="command">Команда на добавление слота приёма к врачу в график поликлиники.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор добавленного слота в графике поликлиники.</returns>
    Task<int> AddAppointmentSlotAsync(
        AddAppoinmentSlotCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить слоты приёмов к врачу в график по шаблону.
    /// </summary>
    /// <param name="command">Команда на добавление слотов приёмов к врачу по шаблону.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task<bool> AddAppointmentSlotsByTemplate(
        AddAppointmentSlotsByTemplateCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Редактировать данные о слоте приёма к врачу в графике.
    /// </summary>
    /// <param name="command">Команда на редактирование данных слота приёма к врачу.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task UpdateAppointmentSlotAsync(
        UpdateAppointmentSlotCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Редактировать статус слота приёма к врачу в графике.
    /// </summary>
    /// <param name="command">Команда на редактирование статуса приёма к врачу в графике поликлиники.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task UpdateAppointmentSlotStatusAsync(
        UpdateAppointmentSlotStatusCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить слот приёма к врачу.
    /// </summary>
    /// <param name="id">Идентификатор слота приёма в графике.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task DeleteAppointmentSlotAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить слоты приёма к врачу в графике по фильтру.
    /// </summary>
    /// <param name="command">Команда на удаление слотов на приёмы к врачу поликлиники по фильтру.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task DeletePolyclinicAppointmentSlotsByFilterAsync(
        DeletePolyclinicAppointmentSlotsByFilterCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Вернуть слот приёма к врачу из графика.
    /// </summary>
    /// <param name="id">Идентификатор слота приёма в графике.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Слот приёма к врачу по идентификатору или <see langword="null"/> если нет.</returns>
    Task<AppointmentSlotDto?> GetAppointmentSlotByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Вернуть все слоты приёмов ко всем врачам поликлиники на дату.
    /// </summary>
    /// <param name="command">Команда на получение всех слотов приёмов к врачам поликлиники на дату.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Слоты приёмов ко всем врачам поликлиники на дату.</returns>
    Task<AppointmentSlotDto[]> GetPolyclinicAppointmentSlotsByDateAsync(
        PolyclinicAppointmentSlotsByDateCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Вернуть все активные слоты приёма к врачу.
    /// </summary>
    /// <param name="command">Команда на получение активных слотов приёмов к врачу.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Активные слоты приёмов к врачу.</returns>
    Task<AppointmentSlotDto[]> GetDoctorActiveAppointmentSlotsAsync(
        DoctorActiveAppointmentSlotsCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Резервация слота пользователем с подтверждением доступа к личным метрикам.
    /// </summary>
    /// <param name="request">Запрос для резервации слота приема.</param>
    /// <returns></returns>
    Task SlotReservationAsync(UserSlotReservationRequest request);

    /// <summary>
    /// Вернуть все слоты приёма пациента.
    /// </summary>
    /// <param name="command">Команда на получение слотов приёма пациента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Слоты приёма пациента.</returns>
    Task<AppointmentSlotDto[]> GetPatientAppointmentSlotsAsync(
        GetPatientAppointmentSlotsCommand command,
        CancellationToken cancellationToken = default);
}