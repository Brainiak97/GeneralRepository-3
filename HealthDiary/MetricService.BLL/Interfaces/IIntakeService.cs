using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Intake;

namespace MetricService.BLL.Interfaces
{
    public interface IIntakeService
    {

        public Task CreateIntakeAsync(IntakeCreateDTO intakeCreateDTO);


        public Task UpdateIntakeAsync(IntakeUpdateDTO intakeUpdateDTO);


        public Task DeleteIntakeAsync(int intakeId);


        public Task<IntakeDTO> GetIntakeByIdAsync(int intakeId);


        public Task<IEnumerable<IntakeDTO>> GetAllIntakeByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);
    }
}
