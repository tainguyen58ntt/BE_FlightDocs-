using FlightDocs.Service.DocumentApi.Models.Dto;
using FlightDocs.Service.DocumentApi.Pagination;

namespace FlightDocs.Service.DocumentApi.Service.IService
{
    public interface IDocumentTypeService
    {
        Task<bool> CreatAsync(DocumentTypeRequest documentTypeRequest);

        Task<DocumentTypeResponse?> GetDocumentTypeByIdAsync(int id);

        Task<Pagination<DocumentTypeResponse>> GetPaginationAsync(int pageIndex, int pageSize);
    }
}
