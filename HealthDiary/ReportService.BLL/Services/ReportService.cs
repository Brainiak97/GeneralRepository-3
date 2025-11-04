using AutoMapper;
using FluentValidation;
using ReportService.BLL.Common.Generators.Factories;
using ReportService.BLL.Data.Commands;
using ReportService.BLL.Interfaces;
using ReportService.DAL.Interfaces.Repositories;
using ReportService.Domain.Models.Entities;
using Shared.Common.Extensions;

namespace ReportService.BLL.Services;

/// <inheritdoc />
internal class ReportService(
    IReportGeneratorFactory reportGeneratorFactory,
    IReportsRepository reportsRepository,
    IValidator<GenerateReportCommand> generateReportCommandValidator,
    IValidator<IServiceCommand> serviceCommandValidator,
    IMapper mapper)
    : IReportService
{
    /// <inheritdoc />
    public async Task<(byte[] Content, string FileName)> GenerateReportAsync(GenerateReportCommand command, CancellationToken cancellationToken)
    {
        await generateReportCommandValidator.ValidateAndThrowAsync(command, cancellationToken);

        var reportGenerator = reportGeneratorFactory.CreateGenerator(command.ReportFormat);
        var report = await reportGenerator.GenerateAsync(
            command.TemplateId,
            command.ReportContent,
            cancellationToken);

        return new (
            report,
            $"report_{command.EntityId}_{DateTime.Now:yyyyMMdd}.{command.ReportFormat.GetDisplayName()}");
    }

    /// <inheritdoc />
    public async Task<Report?> GetReportByIdAsync(int reportId, CancellationToken cancellationToken) =>
        await reportsRepository.GetByIdAsync(reportId);

    /// <inheritdoc />
    public async Task<int> AddReportAsync(AddReportCommand command, CancellationToken cancellationToken)
    {
        await serviceCommandValidator.ValidateAndThrowAsync(command, cancellationToken);
        
        var report = mapper.Map<Report>(command);
        return await reportsRepository.AddAsync(report);
    }

    /// <inheritdoc />
    public async Task UpdateReportAsync(UpdateReportCommand command, CancellationToken cancellationToken)
    {
        await serviceCommandValidator.ValidateAndThrowAsync(command, cancellationToken);
        
        var report = mapper.Map<Report>(command);
        await reportsRepository.UpdateAsync(report);
    }

    /// <inheritdoc />
    public async Task DeleteReportAsync(int reportId, CancellationToken cancellationToken) =>
        await reportsRepository.DeleteAsync(reportId);
}