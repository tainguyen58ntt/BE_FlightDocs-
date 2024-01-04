using FlightDocs.Serivce.AuthApi.Models;
using FlightDocs.Serivce.AuthApi.Models.Dto;
using FlightDocs.Serivce.AuthApi.Models.Dto.Email;
using FlightDocs.Serivce.AuthApi.Service;
using FlightDocs.Serivce.AuthApi.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FlightDocs.Serivce.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IApplicationUserSerivce _applicationUserSerivce;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IAuthService _authService;
        protected ResponseDto _response;


        public AdminApiController(IAuthService authService, IApplicationUserSerivce applicationUserSerivce, UserManager<ApplicationUser> userManager,
            IEmailService emailService)
        {
            _applicationUserSerivce = applicationUserSerivce;
            _userManager = userManager;
            _emailService = emailService;
            _authService = authService;
            _response = new();



        }



        [HttpPost("register")]
        
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            // check valid email
            var validateResult = await _authService.ValidateEmailAsync(model);


            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(x => new { property = x.PropertyName, message = x.ErrorMessage });
                return BadRequest(errors);
            }

            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.Success = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            //await _messageBus.PublishMessage(model.Email, _configuration.GetValue<string>("TopicAndQueueNames:RegisterUserQueue"));
            return Ok(_response);
        }


        [HttpPost("AssignRole")]
      
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.Success = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);

        }


        [HttpDelete("lock-user-by-email")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> LockUserByEmail(string email)
        {
            var user = await _applicationUserSerivce.GetUserByEmailAsync(email);
            if (user == null) return NotFound("Cannot find user with that email");
            if (user != null)
            {
                var terminalUser = await _applicationUserSerivce.TerminalUserByEmailAsync(email);
                if (!terminalUser) return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Terminal User Fail. Error server" });
            }

            return Ok();
        }


        [HttpDelete("lock-multiple-user-by-emails")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> LockUsersByEmail(List<string> emails)
        {
            foreach (var email in emails) {
                var user = await _applicationUserSerivce.GetUserByEmailAsync(email);
                if (user == null) return NotFound($"Cannot find user with that {email}" );
                if (user != null)
                {
                    var terminalUser = await _applicationUserSerivce.TerminalUserByEmailAsync(email);
                    if (!terminalUser) return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Terminal User Fail. Error server" });
                }
            }

         

            return Ok();
        }


        [HttpPut("unlock-user-by-email")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UnlockUserByEmail(string email)
        {
            var user = await _applicationUserSerivce.GetUserByEmailAsync(email);
            if (user == null) return NotFound("Cannot find user with that email");
            if (user != null)
            {
                var terminalUser = await _applicationUserSerivce.UnTerminalUserByEmailAsync(email);
                if (!terminalUser) return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Terminal User Fail. Error server" });
            }

            return Ok();
        }



        [HttpPost("forgot-password")]
        [Authorize(Roles = "ADMIN")]

        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var forgotPasswordlink = Url.Action("ResetPassword", "ApplicationUserApi", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Forgot password link", forgotPasswordlink);
                _emailService.SendEmail(message);
                return StatusCode(StatusCodes.Status200OK, new ResponseDto
                {
                    Success = true,
                    Message = $"Password change request is" +
                    $"sent on email {user.Email}"
                });;



            }
            return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
            {
                Success = false,
                Message = $"Could not sent link "
            });
        }

   
    }
}
