using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    /// <summary>
    /// Предоставляет реализацию валидации данных о типе анализа
    /// </summary>
    /// <seealso cref="AnalysisType" />
    public class AnalysisTypeValidator : IValidator<AnalysisType>
    {
        const short NameMax= 150;
        const short ReferenceValueMale = 150;
        const short ReferenceValueFemale = 150;

        /// <inheritdoc/>       
        public bool Validate(AnalysisType entity, out Dictionary<string, string> errorList)
        {
            errorList = new Dictionary<string, string>();

            if (entity.Name.Length > NameMax)
                errorList.Add(nameof(entity.Name), $"Длина наименования не должна превышать {NameMax}");

            if (entity.ReferenceValueMale?.Length > ReferenceValueMale)
                errorList.Add(nameof(entity.Name), $"Длина эталонного значения для мужчин не должна превышать {ReferenceValueMale} символов");

            if (entity.ReferenceValueFemale?.Length > ReferenceValueFemale)
                errorList.Add(nameof(entity.Name), $"Длина эталонного значения для женщин не должна превышать {ReferenceValueFemale} символов");

            return errorList.Count == 0;
        }
    }
}
