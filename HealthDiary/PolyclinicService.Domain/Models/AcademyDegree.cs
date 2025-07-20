using System.ComponentModel.DataAnnotations;

namespace PolyclinicService.Domain.Models;

/// <summary>
/// Ученая степень врача.
/// </summary>
public enum AcademyDegree : byte
{
    /// <summary>
    /// Кандидат медицинских наук.
    /// </summary>
    [Display(Name = "Кандидат медицинских наук")]
    Candidate = 1,

    /// <summary>
    /// Доктор медицинских наук.
    /// </summary>
    [Display(Name = "Доктор медицинских наук")]
    Doctor = 2,
}