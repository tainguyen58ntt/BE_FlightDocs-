using AutoMapper;
using FlightDocs.Service.DocumentApi.Models;
using FlightDocs.Service.DocumentApi.Models.Dto;
//using FlightDocs.Service.DocumentApi.Pagination;

namespace FlightDocs.Service.DocumentApi.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //CreateMap(typeof(Pagination<>), typeof(Pagination<>));
            //
       

            //
            CreateMap<DocumentTypeRequest, DocumentType>().ReverseMap();

            //


            CreateMap<GroupResponseDto, Group>().ReverseMap();
        }

    }
}
