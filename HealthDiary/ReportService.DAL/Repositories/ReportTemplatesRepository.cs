using ReportService.DAL.Common.InMemoryStorages.Interfaces;
using ReportService.DAL.Interfaces.Repositories;
using ReportService.Domain.Models.Entities;

namespace ReportService.DAL.Repositories;

/// <inheritdoc />
internal class ReportTemplatesRepository(IReportTemplatesInMemoryStorage templatesStorage) : IReportTemplatesRepository
{
    private readonly IReportTemplatesInMemoryStorage _templatesStorage = templatesStorage
                                                                         ?? throw new ArgumentNullException(nameof(templatesStorage));

    /// <inheritdoc />
    public Task<ReportTemplateMetadata?> GetByIdAsync(int templateId) =>
        Task.FromResult(_templatesStorage.GetByKey(templateId));
}