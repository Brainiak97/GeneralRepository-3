using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    /// <summary>
    /// Предоставляет реализацию валидации данных о результате анализа пользователя
    /// </summary>
    /// <seealso cref="AnalysisResult" />
    public class AnalysisResultValidator : IValidator<AnalysisResult>
    {
        /// <inheritdoc/>  
        public bool Validate(AnalysisResult entity, out Dictionary<string, string> errorList)
        {
            errorList = new Dictionary<string, string>();            

            return errorList.Count == 0;
        }
    }
}
