using System.Text.Json;
using QuestPDF.Fluent;
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
    /// Заголовок отчёта.
    /// </summary>
    private static readonly string ReportHeader = typeof(TData).GetTypeDisplayName();

    /// <summary>
    /// Кэш с именами полей отчёта.
    /// </summary>
    protected static readonly Dictionary<string, string> PropertyDisplayNames =
        typeof(TData)
            .GetProperties()
            .ToDictionary(property => property.Name, property => property.GetPropertyDisplayName());

    /// <summary>
    /// Собрать заголовок отчёта.
    /// </summary>
    /// <param name="container"><see cref="IDocumentContainer"/>.</param>
    /// <param name="fontSize">Высота текста заголовка.</param>
    protected virtual void ComposeHeader(IContainer container, int fontSize) =>
        container.Row(row =>
        {
            row
                .RelativeItem()
                .Text(ReportHeader)
                .FontSize(fontSize)
                .AlignCenter()
                .Bold();
        });

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
            throw new FormatException("Ошибка получения данных по отчёту");
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