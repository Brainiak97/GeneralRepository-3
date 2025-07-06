using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    /// <summary>
    ///  Предоставляет реализацию валидации данных о тренировке пользователя
    /// </summary>
    /// <seealso cref="Workout" />
    public class WorkoutValidator : IValidator<Workout>
    {
        /// <inheritdoc/>        
        public bool Validate(Workout entity, out Dictionary<string, string> errorList)
        {

            errorList = new Dictionary<string, string>();
           
            if (entity.EndTime < entity.StartTime)
                errorList.Add(nameof(entity.EndTime), "Время завершения тренировки не может быть раньше времени начала тренировки");           

            return errorList.Count == 0;
        }
    }
}
