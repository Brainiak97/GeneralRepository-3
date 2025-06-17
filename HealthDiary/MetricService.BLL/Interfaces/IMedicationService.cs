using MetricService.BLL.DTO.MedicationDTO;

namespace MetricService.BLL.Interfaces
{
    public interface IMedicationService
    {

        public Task CreateMedicationAsync(MedicationCreateDTO medicationCreateDTO);


        public Task UpdateMedicationAsync(MedicationUpdateDTO medicationUpdateDTO);


        public Task DeleteMedicationAsync(int medicationId);


        public Task<MedicationDTO> GetMedicationByIdAsync(int medicationId);


        public Task<IEnumerable<MedicationDTO>> GetAllMedicationAsync(int pageNum, int pageSize);
    }
}
