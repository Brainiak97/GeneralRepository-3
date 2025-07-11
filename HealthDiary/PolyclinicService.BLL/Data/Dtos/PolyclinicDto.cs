namespace PolyclinicService.BLL.Data.Dtos;

/// <summary>
/// Класс передачи данных по поликлинике.
/// </summary>
public class PolyclinicDto
{
    /// <summary>
    /// Идентификатор поликлинники.
    /// </summary>
    public int Id { get; set; }

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
}