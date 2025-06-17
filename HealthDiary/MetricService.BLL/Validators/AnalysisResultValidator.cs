using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    public class AnalysisResultValidator : IValidator<AnalysisResult>
    {
        public bool Validate(AnalysisResult entity, out Dictionary<string, string> errorList)
        {
            errorList = new Dictionary<string, string>();            

            return errorList.Count == 0;
        }
    }
}
