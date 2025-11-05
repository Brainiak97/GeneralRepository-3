using Shared.EmailService.Common.Data;

namespace Shared.EmailService.Common.Builders;

/// <inheritdoc/>
internal class EmailMessageBuilder : IEmailMessageRequestBuilder
{
    private const string BodyBaseEndPart = "<div style=\"margin-top: 30px; padding-top: 20px; border-top: 1px solid #eee;\">\n С уважением,<br>\n команда Health Diary\n</div>";

    private bool _includeBaseBodyEndPart = false;
    private string _subject;
    private IList<string> _bodyParts;
    private IList<AttachmentData> _attachments;

    public EmailMessageBuilder()
    {
        _subject = "Health Diary App message";
        _bodyParts = [];
        _attachments = [];
    }

    /// <inheritdoc/>
    public IEmailMessageRequestBuilder WithSubject(string subject)
    {
        if (!string.IsNullOrEmpty(subject))
        {
            _subject = subject;    
        }
        
        return this;
    }

    /// <inheritdoc/>
    public IEmailMessageRequestBuilder WithBaseBodyEndPart()
    {
        _includeBaseBodyEndPart = true;
        return this;
    }

    /// <inheritdoc/>
    public IEmailMessageRequestBuilder WithCustomBodyPart(string customBodyPart)
    {
        if (!string.IsNullOrEmpty(customBodyPart))
        {
            _bodyParts.Add(customBodyPart);
        }

        return this;
    }

    /// <inheritdoc/>
    public IEmailMessageRequestBuilder WithAttachments(IList<AttachmentData> attachments)
    {
        if (attachments is { Count: > 0 })
        {
            _attachments = attachments;
        }
        
        return this;
    }

    /// <inheritdoc/>
    public EmailMessageData Build(string to)
    {
        if (string.IsNullOrEmpty(to))
        {
            throw new ArgumentException("Email получателя не должен быть пустым");
        }

        if (_includeBaseBodyEndPart)
        {
            _bodyParts.Add(BodyBaseEndPart);
        }

        var body = string.Join(Environment.NewLine, _bodyParts);

        return new EmailMessageData
        {
            To = to,
            Subject = _subject,
            Body = body,
            Attachments = _attachments.ToList(),
        };
    }
}