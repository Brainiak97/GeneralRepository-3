using EmailService.Api.Contracts;
using ReportService.BLL.Interfaces;
using ReportService.Common.Helpers;
using ReportService.Domain.Models;
using Shared.EmailService.Common.Builders;
using Shared.EmailService.Common.Data;

namespace ReportService.BLL.Services;

/// <inheritdoc/>
internal class EmailSendService(
    IEmailServiceClient emailServiceClient,
    IEmailMessageRequestBuilder emailMessageRequestBuilder)
    : IEmailSendService
{
    private const string SubjectMessage = "Отчёт № ";

    /// <inheritdoc/>
    public async Task SendReportAsync(
        int reportId,
        string emailAddress,
        byte[] report,
        string fileName,
        ReportFormat reportFormat,
        CancellationToken cancellationToken = default)
    {
        var attachment = new AttachmentData(report, ReportServiceHelper.GetContentTypeByFormat(reportFormat), fileName);
        var messageData = emailMessageRequestBuilder
            .WithSubject(SubjectMessage + reportId)
            .WithBaseBodyEndPart()
            .WithAttachments([attachment])
            .Build(emailAddress);

        await emailServiceClient.SendEmailAsync(messageData);
    }
}