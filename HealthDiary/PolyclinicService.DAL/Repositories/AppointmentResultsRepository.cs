using Microsoft.EntityFrameworkCore;
using PolyclinicService.DAL.Contexts;
using PolyclinicService.DAL.Interfaces;
using PolyclinicService.Domain.Models.Entities;

namespace PolyclinicService.DAL.Repositories;

/// <inheritdoc />
internal class AppointmentResultsRepository(PolyclinicServiceDbContext context) : IAppointmentResultsRepository
{
    /// <inheritdoc />
    public async Task<AppointmentResult?> GetByIdAsync(int id) =>
        await context.AppointmentResults
            .AsNoTracking()
            .SingleOrDefaultAsync();

    /// <inheritdoc />
    public async Task<int> AddAsync(AppointmentResult entity)
    {
        var contextEntity = await context.AppointmentResults.AddAsync(entity);
        await context.SaveChangesAsync();
        return contextEntity.Entity.Id;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(AppointmentResult entity)
    {
        context.AppointmentResults.Update(entity);
        return await context.SaveChangesAsync() == 1;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id) =>
        await context.AppointmentResults
            .Where(vr => vr.Id == id)
            .ExecuteDeleteAsync();

    /// <inheritdoc />
    public async Task<IEnumerable<AppointmentResult>?> GetPatientAppointmentResultsWithSlotInfoAsync(int patientId, DateTime? date) =>
        await context.AppointmentResults
            .Include(s => s.AppointmentSlot)
            .Where(s => s.AppointmentSlot.UserId == patientId && (date == null || s.AppointmentSlot.Date == date))
            .AsNoTracking()
            .ToListAsync();
}