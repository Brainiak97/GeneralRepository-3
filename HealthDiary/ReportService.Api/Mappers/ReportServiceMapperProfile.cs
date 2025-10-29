using AutoMapper;
using ReportService.BLL.Data.Commands;
using ReportService.Domain.Models.Entities;

namespace ReportService.Api.Mappers;

/// <summary>
/// Профиль AutoMapper для сервиса отчётов.
/// </summary>
public class ReportServiceMapperProfile : Profile
{
    /// <summary>
    /// Конструктор профиля AutoMapper для сервиса отчётов.
    /// </summary>
    public ReportServiceMapperProfile()
    {
        CreateMap<AddReportCommand, Report>();
        CreateMap<UpdateReportCommand, Report>();

        CreateMap<Report, Report>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}