using PolyclinicService.Domain.Models.Entities;
using Shared.Common.Interfaces;

namespace PolyclinicService.DAL.Interfaces;

/// <summary>
/// Предоставляет методы для работы с результатами приёма к врачу <see cref="AppointmentResult"/> в базы данных.
/// </summary>
public interface IAppointmentResultsRepository : IRepository<AppointmentResult, int>;