namespace ReportService.BLL.Common;

/// <summary>
/// Сообщения ошибок валидации сущностей сервиса.
/// </summary>
internal static class ValidationExceptionMessages
{
    internal const string InvalidEntityIdMessage = "Задан некорректный идентификатор сущности";
    internal const string ReportContentIsEmptyMessage = "Передан пустой отчёт";
    internal const string InvalidTemplateIdMessage = "Задан некорректный идентификатор шаблона отчёта";
    internal const string InvalidFileNameMessage = "Задано некорректное имя файла";
    internal const string InvalidReportIdMessage = "Задан некорректный идентификатор отчёта";
}