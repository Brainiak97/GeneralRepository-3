namespace ReportService.BLL.Data;

/// <summary>
/// Данные для формирования поля отчёта в UI.
/// </summary>
/// <param name="Name">Имя поля.</param>
/// <param name="Type">Тип поля.</param>
/// <param name="DisplayName">Отображаемое имя поля.</param>
/// <param name="MayBeNull">Поле может принимать null.</param>
public record TemplateField(string Name, string Type, string DisplayName, bool MayBeNull);