using MetricService.BLL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Validators
{
    /// <summary>
    /// Предоставляет реализацию валидации данных о физической активности
    /// </summary>
    /// <seealso cref="PhysicalActivity" />
    public class PhysicalActivityValidator : IValidator<PhysicalActivity>
    {
        const short NameMax = 150;

        /// <inheritdoc/>       
        public bool Validate(PhysicalActivity entity, out Dictionary<string, string> errorList)
        {
            errorList = new Dictionary<string, string>();

            if (entity.Name.Length > NameMax)
                errorList.Add(nameof(entity.Name), $"Длина наименования не должна превышать {NameMax}");

            return errorList.Count == 0;
        }
    }
}
