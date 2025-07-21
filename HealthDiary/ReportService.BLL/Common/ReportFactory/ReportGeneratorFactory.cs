using ReportService.Api.Contracts.Enums;
using ReportService.BLL.Common.Generators;

namespace ReportService.BLL.Common.ReportFactory;

/// <inheritdoc />
internal class ReportGeneratorFactory : IReportGeneratorFactory
{
    /// <inheritdoc />
    public IReportGenerator CreateGenerator(ReportFormat reportFormat)
    {
        throw new NotImplementedException();
    }
}