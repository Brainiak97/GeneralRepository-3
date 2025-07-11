using Shared.Common.Interfaces;

namespace PolyclinicService.Domain.Models.Entities;

/// <summary>
/// Данные о поликлиннике.
/// </summary>
public class Polyclinic : IEntityModel<int>
{
    /// <summary>
    /// Идентификатор поликлинники.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Наименование поликлинники.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Адрес.
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Адрес электронной почты.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Ссылка на сайт.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Доктора работающие в поликлинике.
    /// </summary>
    public ICollection<Doctor> PolyclinicDoctors { get; set; } = null!;
}