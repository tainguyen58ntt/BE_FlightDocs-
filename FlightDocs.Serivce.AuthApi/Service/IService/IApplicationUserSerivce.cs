using FlightDocs.Serivce.AuthApi.Models.Dto;

namespace FlightDocs.Serivce.AuthApi.Service.IService
{
    public interface IApplicationUserSerivce
    {
        Task<IEnumerable<UserDto>> GetUserAsync();
        Task<UserDto> GetUserByIdAsync(string id);
        Task<UserDto> GetUserByEmailAsync(string email);

        //Task<bool> ForgotPasswordAsync(string email);
        Task<bool> TerminalUserByEmailAsync(string email);

        Task<bool> UnTerminalUserByEmailAsync(string email);
    }
}
