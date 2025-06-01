using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    public  class HealtMetricBaseValidator : IValidator<HealthMetricsBase>
    {
        public bool Validate(HealthMetricsBase entity, out IDictionary<string, string> errorList)
        {
            errorList = new Dictionary<string, string>();
            
            if (entity.BloodPressureDia > entity.BloodPressureSys)
                errorList.Add(nameof(entity. BloodPressureDia), "Нижнее артериальное давление не может быть больше верхнего артериального давления");

            if (entity.BloodPressureSys < entity.BloodPressureDia)
                errorList.Add(nameof(entity.BloodPressureSys), "Верхнее артериальное давление не модет быть меньше нижнего артериального давления");

            return errorList.Count == 0;
        }
    }
}
