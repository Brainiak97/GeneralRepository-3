using ReportService.Api.Contracts.Enums;
using ReportService.BLL.Interfaces;

namespace ReportService.BLL.Services;

/// <inheritdoc />
internal class ReportService : IReportService
{
    /// <inheritdoc />
    public void GenerateReport<TData>(TData data, ReportFormat format)
    {
        throw new NotImplementedException();
    }
}