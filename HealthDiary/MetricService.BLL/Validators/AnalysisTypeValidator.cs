using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    public  class AnalysisTypeValidator : IValidator<AnalysisType>
    {
        const short NAMEMAX= 150;
        const short ReferenceValueMale = 150;
        const short ReferenceValueFemale = 150;

        public bool Validate(AnalysisType entity, out Dictionary<string, string> errorList)
        {
            errorList = new Dictionary<string, string>();

            if (entity.Name.Length > NAMEMAX)
                errorList.Add(nameof(entity.Name), $"Длина наименования не должна превышать {NAMEMAX}");

            if (entity.ReferenceValueMale?.Length > ReferenceValueMale)
                errorList.Add(nameof(entity.Name), $"Длина эталонного значения для мужчин не должна превышать {ReferenceValueMale}");

            if (entity.ReferenceValueFemale?.Length > ReferenceValueFemale)
                errorList.Add(nameof(entity.Name), $"Длина эталонного значения для женщин не должна превышать {ReferenceValueFemale}");

            return errorList.Count == 0;
        }
    }
}
