using ReportService.DAL.Common.InMemoryStorages.Interfaces;
using ReportService.DAL.Interfaces.Repositories;
using ReportService.Domain.Models.Entities;

namespace ReportService.DAL.Repositories;

/// <inheritdoc />
internal class ReportTemplatesRepository(IReportTemplatesInMemoryStorage templatesStorage) : IReportTemplatesRepository
{
    /// <inheritdoc />
    public Task<ReportTemplateMetadata?> GetByIdAsync(int templateId)
    {
        throw new NotImplementedException();
    }
}