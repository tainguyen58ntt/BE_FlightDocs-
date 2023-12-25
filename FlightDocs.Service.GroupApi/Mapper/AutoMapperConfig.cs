using AutoMapper;
using FlightDocs.Service.GroupApi.Models;
using FlightDocs.Service.GroupApi.Models.Dto;
using FlightDocs.Service.GroupApi.Pagination;

namespace FlightDocs.Service.GroupApi.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
            //
            CreateMap<GroupResponseDto, Group>()
        .ForMember(dest => dest.ApplicationUsers, opt => opt.MapFrom(src => src.ApplicationUserDtos))
        .ReverseMap();
            CreateMap<GroupRequestDto, Group>().ReverseMap();

            //
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
        }

    }
}
