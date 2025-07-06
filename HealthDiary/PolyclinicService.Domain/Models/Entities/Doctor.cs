using Shared.Common.Interfaces;

namespace PolyclinicService.Domain.Models.Entities;

/// <summary>
/// Врач.
/// </summary>
public class Doctor : IEntityModel<int>
{
    /// <summary>
    /// Идентификатор врача в БД (UserId в UserService).
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Стаж врача.
    /// </summary>
    public short Seniority { get; set; }

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
    public bool IsConfirmedEducation { get; set; }

    /// <summary>
    /// Признак, что у врача подтвержден документ о квалификации.
    /// </summary>
    public bool IsConfirmedQualification { get; set; }

    /// <summary>
    /// Специализация врача.
    /// </summary>
    public SpecializationType SpecializationType { get; set; }

    /// <summary>
    /// Навигационное свойство для связи с поликлиникой.
    /// </summary>
    public ICollection<Polyclinic> Polyclinics { get; set; } = null!;
}