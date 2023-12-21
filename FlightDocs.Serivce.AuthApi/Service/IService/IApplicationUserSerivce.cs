using FlightDocs.Serivce.AuthApi.Models.Dto;

namespace FlightDocs.Serivce.AuthApi.Service.IService
{
    public interface IApplicationUserSerivce
    {
        Task<IEnumerable<UserDto>> GetUserAsync();
    }
}
