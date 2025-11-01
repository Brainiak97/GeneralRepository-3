using StateService.Api.ViewModels;

namespace StateService.Domain.Dto
{
    public record  MedicationProgressDto
    {
        public int UserId {  get; init; }

        public required List<RegimenProgressDTO> Regimens { get; init; }
    }
}
