namespace ReportService.Domain.Models;

/// <summary>
/// Форматы отчётов.
/// </summary>
public enum ReportFormat : byte
{
    /// <summary>
    /// Pdf.
    /// </summary>
    Pdf = 1,

    /// <summary>
    /// Xls.
    /// </summary>
    Xls = 2,

    /// <summary>
    /// Xlsx.
    /// </summary>
    Xlsx = 3,

    /// <summary>
    /// Csv.
    /// </summary>
    Csv = 4,

    /// <summary>
    /// Docx.
    /// </summary>
    Docx = 5,

    /// <summary>
    /// Html.
    /// </summary>
    Html = 6,
}