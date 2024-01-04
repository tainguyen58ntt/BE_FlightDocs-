using FlightDocs.Service.DocumentApi.Models.Dto;

namespace FlightDocs.Service.DocumentApi.Service.IService
{
    public interface IGroupService
    {
        Task<GroupResponseDto?> GetGroupByIdAsync(int id);

        Task<GroupResponseDto?> GetGroupByIdInMicroserviceAsync(int id);
    }
}
