using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    public class PhysicalActivityValidator : IValidator<PhysicalActivity>
    {
        public bool Validate(PhysicalActivity entity, out Dictionary<string, string> errorList)
        {
            errorList = new Dictionary<string, string>();                       

            return errorList.Count == 0;
        }
    }
}
