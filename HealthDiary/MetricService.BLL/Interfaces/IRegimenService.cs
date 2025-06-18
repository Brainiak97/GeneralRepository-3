using MetricService.BLL.DTO.Regimen;
using MetricService.BLL.DTO;

namespace MetricService.BLL.Interfaces
{
    public interface IRegimenService
    {
        public Task CreateRegimenAsync(RegimenCreateDTO regimenCreateDTO);


        public Task UpdateRegimenAsync(RegimenUpdateDTO regimenUpdateDTO);


        public Task DeleteRegimenAsync(int regimenId);


        public Task<RegimenDTO> GetRegimenByIdAsync(int regimenId);


        public Task<IEnumerable<RegimenDTO>> GetAllRegimenByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);
    }
}
