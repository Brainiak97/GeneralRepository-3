using Microsoft.EntityFrameworkCore;
using PolyclinicService.DAL.Contexts;
using PolyclinicService.DAL.Interfaces;
using PolyclinicService.Domain.Models.Entities;

namespace PolyclinicService.DAL.Repositories;

/// <inheritdoc />
internal class PolyclinicsRepository(PolyclinicServiceDbContext context) : IPolyclinicsRepository
{
    /// <inheritdoc />
    public async Task<Polyclinic?> GetByIdAsync(int id) =>
        await context.Polyclinics.FindAsync(id);

    /// <inheritdoc />
    public async Task<IEnumerable<Polyclinic>?> GetAllAsync() =>
        await context.Polyclinics.AsNoTracking().ToListAsync();

    /// <inheritdoc />
    public async Task<int> AddAsync(Polyclinic entity)
    {
        var contextEntity = await context.Polyclinics.AddAsync(entity);
        await context.SaveChangesAsync();
        return contextEntity.Entity.Id;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(Polyclinic entity)
    {
        context.Polyclinics.Update(entity);
        return await context.SaveChangesAsync() == 1;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id) =>
        await context.Polyclinics
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();
}