using AutoMapper;
using ReportService.Api.Contracts.Data.Dto;
using ReportService.BLL.Data;
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
        CreateMap<UpdateReportCommand, Report>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ReportId));
        CreateMap<Api.Contracts.Enums.ReportFormat, ReportFormat>().ReverseMap();
        CreateMap<ReportTemplateMetadata, ReportTemplateType>()
            .ConstructUsing((source, _) => new ReportTemplateType(source.Id, source.Name));
        CreateMap<TemplateField, TemplateFieldDto>()
            .ConstructUsing((source, _) =>
                new TemplateFieldDto(source.Name, source.Type, source.DisplayName, source.MayBeNull));
        CreateMap<ReportTemplateType, ReportTemplateTypeDto>();
        CreateMap<Report, Report>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}