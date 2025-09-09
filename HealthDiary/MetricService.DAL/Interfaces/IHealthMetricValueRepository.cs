using MetricService.Domain.Models;

namespace MetricService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с данными о значениях медицинских показателей пользователя
    /// </summary>
    /// <seealso cref="HealthMetricValue" />
    public interface IHealthMetricValueRepository : IRepository<HealthMetricValue>
    {
        /// <summary>
        /// Получение списка значений медицинских показателей пользователя по идентификатору медицинского показателя.
        /// </summary>
        /// <param name="healthMetricId">Идентификатор медицинского показателя пользователя</param>
        /// <returns>Список записей из справочника</returns>
        public Task<IEnumerable<HealthMetricValue>> GetListHealthMetricValueByHealthMetricIdAsync(int healthMetricId);
    }
}
