using AutoMapper;
using UserService.BLL.Dto;
using UserService.Domain.Models;

namespace UserService.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public MapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Username, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.IsEmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
        }
    }
}
