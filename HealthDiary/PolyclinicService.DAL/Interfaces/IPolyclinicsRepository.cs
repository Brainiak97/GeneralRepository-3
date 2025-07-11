using PolyclinicService.Domain.Models.Entities;
using Shared.Common.Interfaces;

namespace PolyclinicService.DAL.Interfaces;

/// <summary>
/// Предоставляет методы для работы с поликлиниками <see cref="Polyclinic"/> в базы данных.
/// </summary>
public interface IPolyclinicsRepository : IRepository<Polyclinic, int>
{
    /// <summary>
    /// Возвращает все зарегистрированные поликлиники.
    /// </summary>
    /// <returns>Коллекция зарегистрированных поликлиник.</returns>
    Task<IEnumerable<Polyclinic>?> GetAllAsync();
}