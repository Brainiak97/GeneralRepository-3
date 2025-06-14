using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    public  class AnalysisTypeValidator : IValidator<AnalysisType>
    {
        public bool Validate(AnalysisType entity, out Dictionary<string, string> errorList)
        {
            throw new NotImplementedException();
        }
    }
}
