using FlightDocs.Service.DocumentApi.Models.Dto;

namespace FlightDocs.Service.DocumentApi.Service.IService
{
    public interface IApplicationUserService
    {
        Task<ApplicationUserDto?> GetUserById(string id);
        Task<ApplicationUserDto?> GetUserByEmail(string email);
    }
}
