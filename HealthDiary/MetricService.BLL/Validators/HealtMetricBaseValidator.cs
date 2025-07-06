using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    /// <summary>
    /// Предоставляет реализацию валидации данных о базовых медицинских показателях пользователя
    /// </summary>
    /// <seealso cref="HealthMetricsBase" />
    public class HealtMetricBaseValidator : IValidator<HealthMetricsBase>
    {
        /// <inheritdoc/>
        public bool Validate(HealthMetricsBase entity, out Dictionary<string, string> errorList)
        {
            errorList = new Dictionary<string, string>();
            
            if (entity.BloodPressureDia > entity.BloodPressureSys)
                errorList.Add(nameof(entity. BloodPressureDia), "Нижнее артериальное давление не может быть больше верхнего артериального давления");            

            return errorList.Count == 0;
        }
    }
}
