using ReportService.DAL.Common.InMemoryStorages.Interfaces;
using ReportService.Domain.Models.Entities;

namespace ReportService.DAL.Common.InMemoryStorages;

/// <inheritdoc />
internal class ReportTemplatesInMemoryStorage : IReportTemplatesInMemoryStorage
{
    private readonly Dictionary<int, ReportTemplateMetadata> _reportTemplates = new();

    /// <inheritdoc />
    public bool TryGetValue(int key, out ReportTemplateMetadata reportTemplateMetadata)
    {
        throw new NotImplementedException();
    }
}