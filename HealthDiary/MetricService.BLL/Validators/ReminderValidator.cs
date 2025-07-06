using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    /// <summary>
    /// Предоставляет реализацию валидации данных о напоминании приема медикаментов
    /// </summary>
    /// <seealso cref="Reminder" />
    public class ReminderValidator : IValidator<Reminder>
    {
        /// <inheritdoc/>  
        public bool Validate(Reminder entity, out Dictionary<string, string> errorList)
        {
            errorList = new Dictionary<string, string>();

            if (entity.RemindAt < DateTime.Now)
                errorList.Add(nameof(entity.RemindAt), "Время напоминания уже прошло");

            return errorList.Count == 0;
        }
    }
}
