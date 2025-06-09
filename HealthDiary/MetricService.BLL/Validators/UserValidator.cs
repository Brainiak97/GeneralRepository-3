using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    public class UserValidator : IValidator<User>
    {
        const short HEIGHTMIN = 50;
        const short HEIGHTMAX = 250;
        const short WEIGHTMIN = 0;
        const short WEIGHTMAX = 400;
        const short AGEMAX = 130;
        const short AGEMIN = 5;

        public bool Validate(User entity, out Dictionary<string, string> errorList)
        {
            errorList = new Dictionary<string, string>();

            if (entity.Height <= HEIGHTMIN || entity.Height > HEIGHTMAX)
                errorList.Add(nameof(entity.Height), $"Параметр роста могут быть заданы в диапазоне {HEIGHTMIN} ... {HEIGHTMAX}");

            if ((entity.Weight <= WEIGHTMIN) || (entity.Weight > WEIGHTMAX))
                errorList.Add(nameof(entity.Weight), $"Параметр веса могут быть заданы в диапазоне {WEIGHTMIN} ... {WEIGHTMAX}");
            var age = DateTime.Now.Year - entity.DateOfBirth.Year;
            if ((age > AGEMAX) || (age < AGEMIN))
                errorList.Add(nameof(entity.DateOfBirth), $"Дата рождения задана некорректно. Возраст может быть в диапазоне {AGEMIN} ... {AGEMAX}");

            return errorList.Count == 0;
        }
    }
}
