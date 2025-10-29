using Microsoft.EntityFrameworkCore;
using ReportService.DAL.Contexts;
using ReportService.DAL.Interfaces.Repositories;
using ReportService.Domain.Models.Entities;

namespace ReportService.DAL.Repositories;

/// <inheritdoc />
internal class ReportsRepository(ReportServiceDbContext dbContext) : IReportsRepository
{
    /// <inheritdoc />
    public async Task<Report?> GetByIdAsync(int id) =>
        await dbContext.Reports
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == id);

    /// <inheritdoc />
    public async Task<int> AddAsync(Report entity)
    {
        var contextEntity = await dbContext.Reports.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return contextEntity.Entity.Id;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(Report entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        dbContext.Reports.Update(entity);
        return await dbContext.SaveChangesAsync() == 1;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id) =>
        await dbContext.Reports
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync();

    /// <inheritdoc />
    public async Task<ReportTemplateMetadata?> GetMetadataByIdAsync(int templateId) =>
        await dbContext.ReportTemplatesMetadata
            .AsNoTracking()
            .SingleOrDefaultAsync(m => m.Id == templateId);
}