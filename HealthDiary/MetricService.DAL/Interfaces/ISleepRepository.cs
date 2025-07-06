using MetricService.Domain.Models;

namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о сне пользователя
    /// </summary>
    /// <seealso cref="Sleep" />
    public interface ISleepRepository : IRepository<Sleep> 
    {        
    }
}
