using PolyclinicService.Domain.Models.Entities;
using Shared.Common.Interfaces;

namespace PolyclinicService.DAL.Interfaces;

/// <summary>
/// Предоставляет методы для работы с врачами <see cref="Doctor"/> в базы данных.
/// </summary>
public interface IDoctorsRepository : IRepository<Doctor, int>
{
    /// <summary>
    /// Предоставить набор врачей по идентификатору поликлиники.
    /// </summary>
    /// <param name="polyclinicId">Идентификатор поликлиники.</param>
    /// <returns>Коллекция докторов работающих в поликлинике.</returns>
    Task<IEnumerable<Doctor>?> GetByPolyclinicId(int polyclinicId);
    
    /// <summary>
    /// Возвращает всех зарегистрированных врачей.
    /// </summary>
    /// <returns>Коллекция докторов, зарегистрированных в сервисе.</returns>
    Task<IEnumerable<Doctor>?> GetAllAsync();
}