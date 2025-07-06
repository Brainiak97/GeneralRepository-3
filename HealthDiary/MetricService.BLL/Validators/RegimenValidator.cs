using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    /// <summary>
    /// Предоставляет реализацию валидации данных о схеме приема медикаментов
    /// </summary>
    /// <seealso cref="Regimen" />
    public class RegimenValidator : IValidator<Regimen>
    {
        /// <inheritdoc/>        
        public bool Validate(Regimen entity, out Dictionary<string, string> errorList)
        {

            errorList = new Dictionary<string, string>();

            if (entity.EndDate < entity.StartDate)
                errorList.Add(nameof(entity.EndDate), "Время окончания приема лекарств не может быть раньше времени начала приема лекарств");

            return errorList.Count == 0;
        }
    }
}
