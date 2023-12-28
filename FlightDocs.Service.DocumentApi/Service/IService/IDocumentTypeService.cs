using FlightDocs.Service.DocumentApi.Models.Dto;

namespace FlightDocs.Service.DocumentApi.Service.IService
{
    public interface IDocumentTypeService
    {
        Task<bool> CreatAsync(DocumentTypeRequest documentTypeRequest);
    }
}
