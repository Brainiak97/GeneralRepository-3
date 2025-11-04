using ReportService.BLL.Data;

namespace ReportService.BLL.Common.DataSources.Containers;

/// <summary>
/// Контейнер для типов источников данных для отчётов.
/// </summary>
public interface IDataSourceInstancesContainer
{
    /// <summary>
    /// Вернуть данные по полям источника данных отчёта.
    /// </summary>
    /// <param name="sourceTypeName">Имя источника данных отчёта.</param>
    /// <returns>Источник данных.</returns>
    List<TemplateField> GetDataSourceTemplateFieldsByName(string sourceTypeName);
}