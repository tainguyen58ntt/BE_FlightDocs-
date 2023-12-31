using FlightDocs.Service.DocumentApi.Models.Dto;

namespace FlightDocs.Service.DocumentApi.Service.IService
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentResponseDto>> GetDocumentByFlightIdAsync(string flightId);
    }
}
