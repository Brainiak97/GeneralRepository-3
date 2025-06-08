using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace MetricService.BLL.Validators
{
    public class UserValidator(IConfiguration configuration) : IValidator<User>
    {
        readonly IConfiguration _configuration = configuration.GetSection($"Validate:{nameof(User)}");
        public bool Validate(User entity, out Dictionary<string, string> errorList)
        {
            errorList = new Dictionary<string, string>();

            if (entity.Height <= _configuration.GetValue<int>("Height:Min") || entity.Height > _configuration.GetValue<int>("Height:Max"))
                errorList.Add(nameof(entity.Height), $"Параметр роста могут быть заданы в диапазоне {_configuration.GetValue<int>("Height:Min")}" +
                                                                $" ... {_configuration.GetValue<int>("Height:Max")}");

            if (entity.Weight <= _configuration.GetValue<int>("Weight:Min") || entity.Weight > _configuration.GetValue<int>("Weight:Max"))
                errorList.Add(nameof(entity.Weight), $"Параметр веса могут быть заданы в диапазоне {_configuration.GetValue<int>("Weight:Min")} ... " +
                                                                 $"{_configuration.GetValue<int>("Weight:Max")}");

            if ((DateTime.Now.Year - entity.DateOfBirth.Year) > _configuration.GetValue<int>("Age:Max") || (DateTime.Now.Year - entity.DateOfBirth.Year) < _configuration.GetValue<int>("Age:Min"))
                errorList.Add(nameof(entity.DateOfBirth), $"Дата рождения задана некорректно. Возраст может быть в диапазоне {_configuration.GetValue<int>("Age:Min")} ... " +
                                                                $"{_configuration.GetValue<int>("Age:Max")}");

            return errorList.Count == 0;
        }
    }
}
