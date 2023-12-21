

using FlightDocs.Serivce.AuthApi.Models.Dto;
using FluentValidation.Results;

namespace FlightDocs.Serivce.AuthApi.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<bool> AssignRole(string email, string roleName);

        Task<ValidationResult> ValidateEmailAsync(RegistrationRequestDto vm);
    }
}
