using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    /// <summary>
    /// Предоставляет реализацию валидации данных о приеме медикаментов пользователем
    /// </summary>
    /// <seealso cref="Intake" />
    public class IntakeValidator : IValidator<Intake>
    {
        /// <inheritdoc/>        
        public bool Validate(Intake entity, out Dictionary<string, string> errorList)
        {

            errorList = new Dictionary<string, string>();

            if (entity.TakenAt> DateTime.Now.AddDays(1))
                errorList.Add(nameof(entity.TakenAt), "Дата приема лекарств еще не наступила");

            return errorList.Count == 0;
        }
    }
}
