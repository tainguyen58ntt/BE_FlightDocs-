using AutoMapper;
using FlightDocs.Service.FlightAPI.Models;
using FlightDocs.Service.FlightAPI.Models.Dto;
using FlightDocs.Service.FlightAPI.Pagination;

namespace FlightDocs.Service.FlightAPI.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
            //
            CreateMap<FlightResponseDto, Flight>().ReverseMap();
        }

    }
}
