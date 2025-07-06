using System.ComponentModel.DataAnnotations;

namespace PolyclinicService.Domain.Models;

/// <summary>
/// Типы квалификаций врачей.
/// </summary>
public enum QualificationType : short
{
    /// <summary>
    /// Первая квалификационная категория.
    /// </summary>
    [Display(Name = "Первая квалификационная категория")]
    First = 1,

    /// <summary>
    /// Вторая квалификационная категория.
    /// </summary>
    [Display(Name = "Вторая квалификационная категория")]
    Second = 2,

    /// <summary>
    /// Высшая квалификационная категория.
    /// </summary>
    [Display(Name = "Высшая квалификационная категория")]
    Highest = 3,
}