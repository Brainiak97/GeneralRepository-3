using MetricService.Domain.Models;

namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о профиле пользователя
    /// </summary>
    /// <seealso cref="User" />
    public interface IUserRepository : IRepository<User> { }
}
