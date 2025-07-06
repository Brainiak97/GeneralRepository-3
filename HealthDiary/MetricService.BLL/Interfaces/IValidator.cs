using MetricService.Domain.Models;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для валидаторов данных моеделй данных
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<T> where T : BaseModel
    {
        /// <summary>
        /// Валидировать модель
        /// </summary>
        /// <param name="entity">Проверяемая модель</param>
        /// <param name="errorList">key-свойство модели, values-описание ошибки</param>
        /// <returns>true - если проверка пройдена</returns>
        public bool Validate(T entity, out Dictionary<string, string> errorList);                
    }
}
