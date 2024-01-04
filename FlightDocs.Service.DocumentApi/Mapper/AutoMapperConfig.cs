using AutoMapper;
using FlightDocs.Service.DocumentApi.Models;
using FlightDocs.Service.DocumentApi.Models.Dto;
using FlightDocs.Service.DocumentApi.Pagination;
//using FlightDocs.Service.DocumentApi.Pagination;

namespace FlightDocs.Service.DocumentApi.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
            //

            CreateMap<DocumentType, DocumentResponseAllDto>()
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.Type))
                .ReverseMap();

            CreateMap<Document, DocumentResponseAllDto>()
         .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType.Type))
         .ReverseMap();





            CreateMap<DocumentTypeRequest, DocumentType>().ReverseMap();
            //
            CreateMap<DocumentResponseDto, Document>().ReverseMap();

            CreateMap<DocumentResponseAllDto, Document>().ReverseMap();
            CreateMap<DocumentTypeResponse, DocumentType>().ReverseMap();

            //


            CreateMap<GroupResponseDto, Group>().ReverseMap();
           
        }

    }
}
