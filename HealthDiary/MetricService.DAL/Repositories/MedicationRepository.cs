using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    public class MedicationRepository : BaseRepository<Medication>, IMedicationRepository
    {
        public MedicationRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        public async override Task<bool> UpdateAsync(Medication item)
        {
            Medication? medication = await GetByIdAsync(item.Id);
            if (medication != null)
            {
                medication.Instruction = item.Instruction;
                medication.DosageFormId = item.DosageFormId;
                medication.Name = item.Name;

            }
            return await _contextDb.SaveChangesAsync() == 1;
        }

        public async override Task<Medication?> GetByIdAsync(int id)
        {
            return await _contextDb.Medications
                .Include(m => m.DosageForm)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async override Task<IEnumerable<Medication>> GetAllAsync()
        {
            return await _contextDb.Medications
                .Include(m => m.DosageForm)
                .ToListAsync();
        }
    }
}
