using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace MetricService.BLL.Validators
{
    public class PhysicalActivityValidator(IConfiguration configuration) : IValidator<PhysicalActivity>
    {
        readonly IConfiguration _configuration = configuration.GetSection($"Validate:{nameof(PhysicalActivity)}");
        public bool Validate(PhysicalActivity entity, out Dictionary<string, string> errorList)
        {
            errorList = new Dictionary<string, string>();

            if (entity.Name.Length>_configuration.GetValue<int>("Name:Max"))
                errorList.Add(nameof(entity.Name), $"Длина наименования не должна превышать {_configuration.GetValue<int>("Name:Max")}");

            return errorList.Count == 0;
        }
    }
}
