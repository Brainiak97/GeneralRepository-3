using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.BLL.Validators
{
    public  class AnalysisCategoryValidator : IValidator<AnalysisCategory>
    {
        const short NAMEMAX = 150;
        public bool Validate(AnalysisCategory entity, out Dictionary<string, string> errorList)
        {
            errorList = new Dictionary<string, string>();

            if (entity.Name.Length > NAMEMAX)
                errorList.Add(nameof(entity.Name), $"Длина наименования не должна превышать {NAMEMAX}");

            return errorList.Count == 0;
        }
    }
}
