using FlightDocs.Service.GroupApi.Models.Dto;

namespace FlightDocs.Service.GroupApi.Service.IService
{
    public interface IApplicationUserService
    {
        Task<ApplicationUserDto?> GetUserById(string id);

        Task<int?> GetGroupIdByUserIdInMircoService(string id);
        Task<ApplicationUserDto?> GetUserByEmail(string email);
    }
}
