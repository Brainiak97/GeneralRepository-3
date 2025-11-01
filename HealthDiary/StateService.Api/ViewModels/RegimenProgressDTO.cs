using MetricService.Api.Contracts.Dtos.Intake;
using MetricService.Api.Contracts.Dtos.Regimen;

namespace StateService.Api.ViewModels
{
    public record RegimenProgressDTO: RegimenDTO
    {
        public required List<IntakeDTO> Intakes { get; init; }
    }
}
