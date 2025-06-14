using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    public class SleepValidator: IValidator<Sleep>
    {
        const short QUALITYRATINGMIN = 1;
        const short QUALITYRATINGMAX = 5;
       
        public bool Validate(Sleep entity, out Dictionary<string, string> errorList)
        {

            errorList = new Dictionary<string, string>();

            if (entity.EndSleep < entity.StartSleep)
                errorList.Add(nameof(entity.EndSleep), "Время завершения сна не может быть раньше времени начала сна");

            if ((entity.QualityRating < QUALITYRATINGMIN) || (entity.QualityRating > QUALITYRATINGMAX))
                errorList.Add(nameof(entity.EndSleep), $"Качество сна должно быть в диапазоне " +
                                                    $"{QUALITYRATINGMIN} ... {QUALITYRATINGMAX}");

            return errorList.Count == 0;
        }
    }
}
