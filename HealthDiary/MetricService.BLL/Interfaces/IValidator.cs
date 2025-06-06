using MetricService.Domain.Models;

namespace MetricService.BLL.Interfaces
{
    public  interface IValidator<T> where T : BaseModel
    {
        /// <summary>
        /// Валидатор модели
        /// </summary>
        /// <param name="entity">Проверяемая модель</param>
        /// <param name="errorList">key-свойство модели, values-описание ошибки</param>
        /// <returns></returns>
        public bool Validate(T entity, out Dictionary<string, string> errorList);                
    }
}
