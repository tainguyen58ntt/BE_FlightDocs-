using FluentValidation;

namespace FlightDocs.Serivce.AuthApi.Models.Dto
{
    public class RegistrationRequestDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
    }
    public class RegistrationRequestRule : AbstractValidator<RegistrationRequestDto>
    {
        public RegistrationRequestRule()
        {
            RuleFor(x => x.Email)
        .NotEmpty()
        .WithMessage("Email must not be empty")
        .Must(email => email.EndsWith("@vietjetair.com"))
        .WithMessage("Email must end with @vietjetair.com");







        }
    }
}
