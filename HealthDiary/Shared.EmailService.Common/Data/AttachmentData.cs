namespace Shared.EmailService.Common.Data;

/// <summary>
/// Данные по вложенному файлу письма.
/// </summary>
/// <param name="Content">Содержимое файла.</param>
/// <param name="ContentType">Тип файла.</param>
/// <param name="FileName">Имя файла с расширением.</param>
public record AttachmentData(byte[] Content, string ContentType, string FileName);
