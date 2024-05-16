using AutoMapper;
using ControllerApiTutorial.Models;
using MinimalApiTutorial.Model;

namespace MinimalApiTutorial.Mapper
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserVo, UserEntity>()
                .ForMember(dest => dest.User_Name, opt => { opt.MapFrom(src => src.Name); })
                .ForMember(dest => dest.Nick_name, opt => { opt.MapFrom(src => src.NickName); })
                .ForMember(dest => dest.Phone, opt => { opt.MapFrom(src => src.Cellphone); })
                .ForMember(dest => dest.Pwd, opt => { opt.MapFrom(src => src.Password); });
            CreateMap<UserEntity, UserVo>()
                .ForMember(dest => dest.Name, opt => { opt.MapFrom(src => src.User_Name); })
                .ForMember(dest => dest.NickName, opt => { opt.MapFrom(src => src.Nick_name); })
                .ForMember(dest => dest.Cellphone, opt => { opt.MapFrom(src => src.Phone); })
                .ForMember(dest => dest.Password, opt => { opt.MapFrom(src => src.Pwd); });
        }
    }
}
