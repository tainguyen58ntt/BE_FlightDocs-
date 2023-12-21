using FlightDocs.Service.GroupApi.Models.Dto;

namespace FlightDocs.Service.GroupApi.Service.IService
{
    public interface IApplicationUserService
    {
        Task<IEnumerable<ApplicationUserDto>> GetUserNameById(string id);
    }
}
