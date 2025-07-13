using PolyclinicService.Domain.Models;

namespace PolyclinicService.BLL.Data.Requests;

/// <summary>
/// Запрос на добавление врача в БД.
/// </summary>
public class AddDoctorRequest
{
    /// <summary>
    /// Идентификатор врача в UserService.
    /// </summary>
    public required int UserId { get; set; }

    /// <summary>
    /// Стаж врача (лет).
    /// </summary>
    public byte Seniority { get; set; }

    /// <summary>
    /// Квалификация врача.
    /// </summary>
    public QualificationType QualificationType { get; set; }

    /// <summary>
    /// Научная степень врача (если есть).
    /// </summary>
    public AcademyDegree? AcademyDegree { get; set; }

    /// <summary>
    /// Признак, что у врача подтвержден документ об образовании.
    /// </summary>
    public bool? IsConfirmedEducation { get; set; }

    /// <summary>
    /// Признак, что у врача подтвержден документ о квалификации.
    /// </summary>
    public bool? IsConfirmedQualification { get; set; }

    /// <summary>
    /// Специализация врача.
    /// </summary>
    public SpecializationType SpecializationType { get; set; }   
}