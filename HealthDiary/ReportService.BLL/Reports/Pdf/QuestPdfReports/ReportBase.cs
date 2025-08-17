using System.Text.Json;
using QuestPDF.Infrastructure;
using ReportService.Api.Contracts.Data.Interfaces;
using ReportService.BLL.Reports.Pdf.QuestPdfReports.Interfaces;
using Shared.Common.Extensions;

namespace ReportService.BLL.Reports.Pdf.QuestPdfReports;

/// <summary>
/// Базовый класс шаблона отчёта.
/// </summary>
/// <typeparam name="TData"></typeparam>
public abstract class ReportTemplateBase<TData> : IReportTemplate
    where TData : IReportData
{
    /// <summary>
    /// Кэш с именами полей отчёта.
    /// </summary>
    protected static readonly Dictionary<string, string> PropertyDisplayNames =
        typeof(TData)
            .GetProperties()
            .ToDictionary(property => property.Name, property => property.GetPropertyDisplayName());

    /// <summary>
    /// Скомпилировать отчёту по шаблону.
    /// </summary>
    /// <param name="container"><see cref="IDocumentContainer"/>.</param>
    /// <param name="data">Данные по отчёту (json).</param>
    public void Compile(IDocumentContainer container, string data)
    {
        ArgumentNullException.ThrowIfNull(container);

        if (string.IsNullOrEmpty(data))
            return;

        var dto = JsonSerializer.Deserialize<TData>(data);
        if (dto is null)
        {
            throw new InvalidOperationException("Ошибка получения данных по отчёту");
        }

        Compose(container, dto);
    }

    /// <summary>
    /// Собрать документ.
    /// </summary>
    /// <param name="container"><see cref="IDocumentContainer"/>.</param>
    /// <param name="data">Модель данных отчёта.</param>
    protected abstract void Compose(IDocumentContainer container, TData data);
}