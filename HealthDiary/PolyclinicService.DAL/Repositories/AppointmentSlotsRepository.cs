using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolyclinicService.DAL.Contexts;
using PolyclinicService.DAL.Interfaces;
using PolyclinicService.Domain.Models.Entities;
using Shared.Common.Exceptions;

namespace PolyclinicService.DAL.Repositories;

/// <inheritdoc />
internal class AppointmentSlotsRepository(
    PolyclinicServiceDbContext context,
    IMapper mapper)
    : IAppointmentSlotsRepository
{
    /// <inheritdoc />
    public async Task<AppointmentSlot?> GetByIdAsync(int id) =>
        await context.AppointmentSlots.FindAsync(id);

    /// <inheritdoc />
    public async Task<IEnumerable<AppointmentSlot>?> GetByFilterAsync(Expression<Func<AppointmentSlot, bool>> filter)
    {
        ArgumentNullException.ThrowIfNull(filter);
        return await context.AppointmentSlots.AsNoTracking().Where(filter).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<int> AddAsync(AppointmentSlot entity)
    {
        var contextEntity = await context.AppointmentSlots.AddAsync(entity);
        await context.SaveChangesAsync();
        return contextEntity.Entity.Id;
    }

    /// <inheritdoc />
    public async Task<bool> AddBatchAsync(AppointmentSlot[] appointmentSlots)
    {
        await context.AppointmentSlots.AddRangeAsync(appointmentSlots);
        return await context.SaveChangesAsync() == appointmentSlots.Length;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(AppointmentSlot entity)
    {
        var contextEntity = await context.AppointmentSlots.FindAsync(entity.Id);
        if (contextEntity is null)
        {
            throw new EntryNotFoundException("Слот приёма к врачу не найден");
        }

        contextEntity = mapper.Map(entity, contextEntity);
        context.AppointmentSlots.Update(contextEntity);
        return await context.SaveChangesAsync() == 1;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id) =>
        await context.AppointmentSlots
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();

    /// <inheritdoc />
    public async Task DeleteByFilterAsync(Expression<Func<AppointmentSlot, bool>> filter)
    {
        ArgumentNullException.ThrowIfNull(filter);
        await context.AppointmentSlots.Where(filter).ExecuteDeleteAsync();
    }
}