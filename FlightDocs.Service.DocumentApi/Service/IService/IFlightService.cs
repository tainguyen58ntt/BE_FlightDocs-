using FlightDocs.Service.DocumentApi.Models.Dto;

namespace FlightDocs.Service.DocumentApi.Service.IService
{
    public interface IFlightService
    {

        Task<FlightResponseDto?> GetFlightByIdAsync(string id);

    }
}
