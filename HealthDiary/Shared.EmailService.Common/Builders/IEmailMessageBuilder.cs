using Shared.EmailService.Common.Data;

namespace Shared.EmailService.Common.Builders;

/// <summary>
/// Строитель запроса для отправки в EmailService.
/// </summary>
public interface IEmailMessageRequestBuilder
{
    /// <summary>
    /// Добавить тему сообщения.
    /// </summary>
    /// <param name="subject">Тема сообщения.</param>
    public IEmailMessageRequestBuilder WithSubject(string subject);

    /// <summary>
    /// Добавить базовую часть в конце письма.
    /// </summary>
    public IEmailMessageRequestBuilder WithBaseBodyEndPart();

    /// <summary>
    /// Добавить кастомную часть письма (html).
    /// </summary>
    /// <param name="customBodyPart">Кастомная часть письма.</param>
    public IEmailMessageRequestBuilder WithCustomBodyPart(string customBodyPart);

    /// <summary>
    /// Добавить вложения.
    /// </summary>
    /// <param name="attachments">Вложенные файлы.</param>
    /// <returns></returns>
    public IEmailMessageRequestBuilder WithAttachments(IList<AttachmentData> attachments);

    /// <summary>
    /// Собрать запрос.
    /// </summary>
    /// <param name="to">Email получателя.</param>
    public EmailMessageData Build(string to);
}