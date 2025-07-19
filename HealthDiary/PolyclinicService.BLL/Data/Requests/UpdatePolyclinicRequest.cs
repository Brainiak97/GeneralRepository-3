namespace PolyclinicService.BLL.Data.Requests;

/// <summary>
/// Запрос на редактирование данных о поликлинике.
/// </summary>
public class UpdatePolyclinicRequest
{
    /// <summary>
    /// Идентификатор поликлиники.
    /// </summary>
    public required int PolyclinicId { get; set; }
    
    /// <summary>
    /// Наименование поликлиники.
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Адрес.
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string? PhoneNumber { get; set; }
    
    /// <summary>
    /// Адрес электронной почты.
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Ссылка на сайт.
    /// </summary>
    public string? Url { get; set; }
}