using Microsoft.EntityFrameworkCore;
using PolyclinicService.DAL.Contexts;
using PolyclinicService.DAL.Interfaces;
using PolyclinicService.Domain.Models.Entities;

namespace PolyclinicService.DAL.Repositories;

/// <inheritdoc />
internal class DoctorsRepository(PolyclinicServiceDbContext context) : IDoctorsRepository
{
    /// <inheritdoc />
    public async Task<Doctor?> GetByIdAsync(int id) =>
        await context.Doctors.FindAsync(id);

    /// <inheritdoc />
    public async Task<IEnumerable<Doctor>?> GetAllAsync() =>
        await context.Doctors.AsNoTracking().ToListAsync();

    /// <inheritdoc />
    public async Task<IEnumerable<Doctor>?> GetByPolyclinicId(int polyclinicId) =>
        await context.Doctors
            .AsNoTracking()
            .Where(d => d.Polyclinics.Any(p => p.Id == polyclinicId))
            .ToListAsync();

    /// <inheritdoc />
    public async Task<int> AddAsync(Doctor entity)
    {
        var contextEntity = await context.Doctors.AddAsync(entity);
        await context.SaveChangesAsync();
        return contextEntity.Entity.Id;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(Doctor entity)
    {
        context.Doctors.Update(entity);
        return await context.SaveChangesAsync() == 1;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id) =>
        await context.Doctors
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();
}