namespace PolyclinicService.BLL.Data.Requests;

/// <summary>
/// Запрос на добавление поликлиники.
/// </summary>
public class AddPolyclinicRequest
{
    /// <summary>
    /// Наименование поликлиники.
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