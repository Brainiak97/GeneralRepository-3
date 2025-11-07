namespace PolyclinicService.BLL.Data.Commands;

/// <summary>
/// Команда на получение слотов приёма пациента.
/// </summary>
/// <param name="PatientId">Идентификатор пациента.</param>
/// <param name="StartDate">Дата с которой необходимо получить слоты.</param>
/// <param name="EndDate">Дата по которую необходимо получить слоты.</param>
public record GetPatientAppointmentSlotsCommand(
    int PatientId,
    DateTime? StartDate,
    DateTime? EndDate);