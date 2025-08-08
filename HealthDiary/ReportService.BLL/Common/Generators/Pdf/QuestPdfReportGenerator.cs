using ReportService.BLL.Common.Interfaces;

namespace ReportService.BLL.Common.Generators.Pdf;

internal class QuestPdfReportGenerator : IPdfReportGenerator
{
    public byte[] Generate<TData>(TData reportData) where TData : IReportData
    {
        throw new NotImplementedException();
    }
}