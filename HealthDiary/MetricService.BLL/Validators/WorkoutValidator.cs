using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    public  class WorkoutValidator : IValidator<Workout>
    {
        public bool Validate(Workout entity, out IDictionary<string, string> errorList)
        {

            errorList = new Dictionary<string, string>();
           
            if (entity.EndTime < entity.StartTime)
                errorList.Add(nameof(entity.EndTime), "Время завершения тренировки не может быть раньше времени начала тренировки");

            if (entity.StartTime > entity.EndTime)
                errorList.Add(nameof(entity.StartTime), "Время начала тренировки не может быть позже времени окончания тренировки");

            return errorList.Count == 0;
        }
    }
}
