using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    public  class UserValidator : IValidator<User>
    {
        public bool Validate(User entity, out IDictionary<string, string> errorList)
        {
           errorList = new Dictionary<string, string>();

            if (entity.Height <= 50 || entity.Height > 250)
                errorList.Add(nameof(entity.Height), "Параметр роста заданы некорректно");

            if (entity.Weight <= 0 || entity.Weight > 400)
                errorList.Add(nameof(entity.Weight), "Параметр веса заданы некорректно");

            if ((DateTime.Now.Year - entity.DateOfBirth.Year) > 130 || (DateTime.Now.Year - entity.DateOfBirth.Year) < 5)
                errorList.Add(nameof(entity.DateOfBirth), "Дата рождения задана некорректно");

            return errorList.Count == 0;
        }
    }
}
