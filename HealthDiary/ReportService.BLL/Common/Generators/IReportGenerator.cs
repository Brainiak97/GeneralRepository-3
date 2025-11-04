namespace ReportService.BLL.Common.Generators;

/// <summary>
/// Генератор отчёта.
/// </summary>
public interface IReportGenerator
{
    /// <summary>
    /// Сгенерировать отчёт.
    /// </summary>
    /// <param name="templateId">Идентификатор шаблона отчёта.</param>
    /// <param name="reportData">Содержимое отчёта (json).</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Сгенерированный отчёт.</returns>
    Task<byte[]> GenerateAsync(int templateId, string reportData, CancellationToken cancellationToken);
}