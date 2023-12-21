using FlightDocs.Serivce.AuthApi.Models.Dto;
using FlightDocs.Serivce.AuthApi.Service.IService;
using FluentValidation;

namespace FlightDocs.Serivce.AuthApi.Service
{
    public class UserValidator : IUserValidator
    {
        private readonly RegistrationRequestRule _userSignUpValidator;


        public UserValidator(RegistrationRequestRule userSignUpValidator)
        {
            _userSignUpValidator = userSignUpValidator;
           
        }

        public IValidator<RegistrationRequestDto> RegistrationRequestValidator => _userSignUpValidator;
    }
}
