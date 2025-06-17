using MetricService.BLL.DTO.Workout;
using MetricService.BLL.DTO.BaseModel;

namespace MetricService.BLL.Interfaces
{
    public  interface IDosageFormService
    {
        
        public Task CreateDosageFormAsync(DosageFormCreateDTO dosageFormCreateDTO);

        
        public Task UpdateDosageFormAsync(DosageFormUpdateDTO dosageFormUpdateDTO);

        
        public Task DeleteDosageFormAsync(int dosageFormId);

       
        public Task<DosageFormDTO> GetDosageFormByIdAsync(int dosageFormId);

        
        public Task<IEnumerable<DosageFormDTO>> GetAllDosageFormsAsync(int pageNum, int pageSize);
    }
}
