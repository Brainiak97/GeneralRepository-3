using QuestPDF.Infrastructure;

namespace ReportService.BLL.Common.Interfaces;

/// <summary>
/// Модель данных включащая содержимое отчёта и метаданные для его генерации.
/// </summary>
public interface IReport : IDocument;