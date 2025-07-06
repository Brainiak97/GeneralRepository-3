using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о медикаментах
    /// </summary>
    /// <seealso cref="BaseRepository&lt;Medication&gt;" />
    /// <seealso cref="IMedicationRepository" />
    public class MedicationRepository : BaseRepository<Medication>, IMedicationRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория<see cref="MedicationRepository"/> class.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public MedicationRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb)
        {
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async override Task<Medication?> GetByIdAsync(int id)
        {
            return await _contextDb.Medications
                .Include(m => m.DosageForm)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        /// <inheritdoc/>
        public async override Task<IEnumerable<Medication>> GetAllAsync()
        {
            return await _contextDb.Medications
                .Include(m => m.DosageForm)
                .ToListAsync();
        }
    }
}
