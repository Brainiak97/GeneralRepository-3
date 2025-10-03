using ReportService.Api.Contracts.Data.Dto;
using ReportService.DAL.Common.InMemoryStorages.Interfaces;
using ReportService.Domain.Models.Entities;
using Shared.Common.Extensions;

namespace ReportService.DAL.Common.InMemoryStorages;

/// <inheritdoc />
internal class ReportTemplatesInMemoryStorage : IReportTemplatesInMemoryStorage
{
    private readonly Dictionary<int, ReportTemplateMetadata> _reportTemplates = new()
    {
        {
            1,
            new ReportTemplateMetadata
            {
                Name = typeof(CardiologistReportDataDto).GetTypeDisplayName(),
                ReportTemplateTypeName = "CardiologistReportTemplate",
            }
        },
    };

    /// <inheritdoc />
    public ReportTemplateMetadata? GetByKey(int key) => _reportTemplates.GetValueOrDefault(key);
}