using System.ComponentModel.DataAnnotations;

namespace ReportService.Api.Contracts.Enums;

/// <summary>
/// Форматы отчётов.
/// </summary>
public enum ReportFormat
{
    /// <summary>
    /// Pdf.
    /// </summary>
    [Display(Name = "pdf")]
    Pdf = 1,

    /// <summary>
    /// Xls.
    /// </summary>
    [Display(Name = "xls")]
    Xls = 2,

    /// <summary>
    /// Xlsx.
    /// </summary>
    [Display(Name = "xlsx")]
    Xlsx = 3,

    /// <summary>
    /// Csv.
    /// </summary>
    [Display(Name = "csv")]
    Csv = 4,

    /// <summary>
    /// Docx.
    /// </summary>
    [Display(Name = "docx")]
    Docx = 5,

    /// <summary>
    /// Html.
    /// </summary>
    [Display(Name = "html")]
    Html = 6,
}