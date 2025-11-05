namespace Shared.EmailService.Common.Data;

/// <summary>
/// Данные электронного письма.
/// </summary>
public record EmailMessageData
{
    /// <summary>
    /// Адрес электронной почты получателя.
    /// </summary>
    public required string To { get; init; }

    /// <summary>
    /// Тема письма.
    /// </summary>
    public required string Subject { get; init; }

    /// <summary>
    /// Тело письма в формате HTML.
    /// </summary>
    public required string Body { get; init; }

    /// <summary>
    /// Вложения в письмо
    /// </summary>
    public List<AttachmentData> Attachments { get; init; } = [];
}