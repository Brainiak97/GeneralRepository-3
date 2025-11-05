using AutoMapper;
using ReportService.BLL.Data.Commands;
using ReportService.Domain.Models;
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
        CreateMap<AddReportCommand, Report>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ReportId));
        CreateMap<UpdateReportCommand, Report>();
        CreateMap<Api.Contracts.Enums.ReportFormat, ReportFormat>().ReverseMap();
        CreateMap<Report, Report>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}