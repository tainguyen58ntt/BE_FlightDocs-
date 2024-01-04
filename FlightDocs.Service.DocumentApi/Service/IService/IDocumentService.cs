using FlightDocs.Service.DocumentApi.Models.Dto;
using FlightDocs.Service.DocumentApi.Pagination;

namespace FlightDocs.Service.DocumentApi.Service.IService
{
    public interface IDocumentService
    {
    
        Task<IEnumerable<DocumentResponseDto>> GetDocumentByFlightIdAsync(string flightId);

        Task<IEnumerable<DocumentResponseDto>> GetDocumentWasUpdatedByUser(string flightId);

        
        Task<DocumentResponseDto?> GetDocumentByIdAsync(int id);
        Task<Pagination<DocumentResponseAllDto>> GetPaginationAsync(int pageIndex, int pageSize);

        Task<bool> CreateDocumentForFlight(string flighId, DocumentUploadModel documentUploadModel, int documentTypeId);
    }
}
