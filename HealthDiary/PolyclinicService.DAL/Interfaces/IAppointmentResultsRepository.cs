using PolyclinicService.Domain.Models.Entities;
using Shared.Common.Interfaces;

namespace PolyclinicService.DAL.Interfaces;

/// <summary>
/// Предоставляет методы для работы с результатами приёма к врачу <see cref="AppointmentResult"/> в базы данных.
/// </summary>
public interface IAppointmentResultsRepository : IRepository<AppointmentResult, int>
{
    /// <summary>
    /// Вернуть результаты приёма пациента с данными слота графика поликлинники.
    /// </summary>
    /// <param name="patientId">Идентификатор пациента (пользователя).</param>
    /// <param name="date">Дата, на которую необходимо получить результаты (по необходимости).</param>
    /// <returns>Результаты приёма пациента с данными из графика.</returns>
    Task<IEnumerable<AppointmentResult>?> GetPatientAppointmentResultsWithSlotInfoAsync(int patientId, DateTime? date);
}