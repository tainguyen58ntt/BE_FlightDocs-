using FlightDocs.Serivce.AuthApi.Models.Dto;
using FluentValidation;

namespace FlightDocs.Serivce.AuthApi.Service.IService
{
    public interface IUserValidator
    {
        IValidator<RegistrationRequestDto> RegistrationRequestValidator { get; }
    }
}
