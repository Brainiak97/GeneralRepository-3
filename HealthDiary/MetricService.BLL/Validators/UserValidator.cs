using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    /// <summary>
    /// Предоставляет реализацию валидации данных о профиле пользователя
    /// </summary>
    /// <seealso cref="User" />
    public class UserValidator : IValidator<User>
    {
        const short HeightMin = 50;
        const short HeightMax = 250;
        const short WeightMin = 0;
        const short WeightMax = 400;
        const short AgeMax = 130;
        const short AgeMin = 5;

        /// <inheritdoc/>  
        public bool Validate(User entity, out Dictionary<string, string> errorList)
        {
            errorList = new Dictionary<string, string>();

            if (entity.Height <= HeightMin || entity.Height > HeightMax)
                errorList.Add(nameof(entity.Height), $"Параметр роста могут быть заданы в диапазоне {HeightMin} ... {HeightMax}");

            if ((entity.Weight <= WeightMin) || (entity.Weight > WeightMax))
                errorList.Add(nameof(entity.Weight), $"Параметр веса могут быть заданы в диапазоне {WeightMin} ... {WeightMax}");
            var age = DateTime.Now.Year - entity.DateOfBirth.Year;
            if ((age > AgeMax) || (age < AgeMin))
                errorList.Add(nameof(entity.DateOfBirth), $"Дата рождения задана некорректно. Возраст может быть в диапазоне {AgeMin} ... {AgeMax}");

            return errorList.Count == 0;
        }
    }
}
