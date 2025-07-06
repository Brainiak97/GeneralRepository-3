using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PolyclinicService.DAL.Contexts;
using PolyclinicService.DAL.Interfaces;
using PolyclinicService.Domain.Models.Entities;

namespace PolyclinicService.DAL.Repositories;

/// <inheritdoc />
internal class AppointmentSlotsRepository(PolyclinicServiceDbContext context) : IAppointmentSlotsRepository
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
            return false;
        }
        
        context.AppointmentSlots.Update(FillSlotFieldsForUpdate(contextEntity, entity));
        return await context.SaveChangesAsync() == 1;
    }

    public async Task<bool> UpdateByFilterAsync(Expression<Func<AppointmentSlot, bool>> filter, AppointmentSlot entity)
    {
        ArgumentNullException.ThrowIfNull(filter);
        ArgumentNullException.ThrowIfNull(entity);

        var contextEntities = context.AppointmentSlots.AsNoTracking().Where(filter);
        context.AppointmentSlots.UpdateRange(contextEntities.Select(e => FillSlotFieldsForUpdate(entity, e)));
        return await context.SaveChangesAsync() > 0;
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

    private AppointmentSlot FillSlotFieldsForUpdate(AppointmentSlot sourceEntity, AppointmentSlot targetEntity)
    {
        targetEntity.DoctorId = sourceEntity.DoctorId;
        targetEntity.UserId = sourceEntity.UserId;
        targetEntity.Date = sourceEntity.Date;
        targetEntity.StartTime = sourceEntity.StartTime;
        targetEntity.EndTime = sourceEntity.EndTime;
        targetEntity.EndTime = sourceEntity.EndTime;
        targetEntity.Status = sourceEntity.Status;
        return targetEntity;
    }
}