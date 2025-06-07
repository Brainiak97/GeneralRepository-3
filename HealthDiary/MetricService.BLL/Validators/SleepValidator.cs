using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    public class SleepValidator : IValidator<Sleep>
    {
        public bool Validate(Sleep entity, out Dictionary<string, string> errorList)
        {
            
            errorList =new Dictionary<string, string>();
            
            if (entity.EndSleep < entity.StartSleep)
                errorList.Add(nameof(entity.EndSleep), "Время завершения сна не может быть раньше времени начала сна");            

            return errorList.Count == 0;
        }
    }
}
