using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace MetricService.BLL.Validators
{
    public class SleepValidator(IConfiguration configuration) : IValidator<Sleep>
    {
        readonly IConfiguration _configuration = configuration.GetSection($"Validate:{nameof(Sleep)}");
        public bool Validate(Sleep entity, out Dictionary<string, string> errorList)
        {

            errorList = new Dictionary<string, string>();

            if (entity.EndSleep < entity.StartSleep)
                errorList.Add(nameof(entity.EndSleep), "Время завершения сна не может быть раньше времени начала сна");

            if (entity.QualityRating < _configuration.GetValue<int>("QualityRating:Min") || entity.QualityRating > _configuration.GetValue<int>("QualityRating:Min"))
                errorList.Add(nameof(entity.EndSleep), $"Качество сна должно быть в диапазоне " +
                                                    $"{_configuration.GetValue<int>("QualityRating:Min")} ... {_configuration.GetValue<int>("QualityRating:Min")}");

            return errorList.Count == 0;
        }
    }
}
