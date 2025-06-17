using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    public class ReminderValidator : IValidator<Reminder>
    {
        public bool Validate(Reminder entity, out Dictionary<string, string> errorList)
        {

            errorList = new Dictionary<string, string>();

            if (entity.RemindAt < DateTime.Now)
                errorList.Add(nameof(entity.RemindAt), "Время напоминания уже прошло");

            return errorList.Count == 0;
        }
    }
}
