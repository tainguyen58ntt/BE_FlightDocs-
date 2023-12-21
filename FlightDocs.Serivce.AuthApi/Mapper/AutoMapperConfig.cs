using AutoMapper;
using FlightDocs.Serivce.AuthApi.Models;
using FlightDocs.Serivce.AuthApi.Models.Dto;
using FlightDocs.Service.AuthApi.Pagination;

namespace FlightDocs.Service.AuthApi.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
            //
            CreateMap<UserDto, ApplicationUser>().ReverseMap();
        }

    }
}
