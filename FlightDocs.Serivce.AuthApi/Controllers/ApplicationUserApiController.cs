using FlightDocs.Serivce.AuthApi.Models;
using FlightDocs.Serivce.AuthApi.Models.Dto;
using FlightDocs.Serivce.AuthApi.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs.Serivce.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserApiController : ControllerBase
    {
        private readonly IApplicationUserSerivce _applicationUserSerivce;
        private readonly UserManager<ApplicationUser> _userManager;


        public ApplicationUserApiController(IApplicationUserSerivce applicationUserSerivce,UserManager<ApplicationUser> userManager)
        {
            _applicationUserSerivce = applicationUserSerivce;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _applicationUserSerivce.GetUserAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            return Ok(await _applicationUserSerivce.GetUserByIdAsync(id));
        }

        [HttpGet("get-by-email")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _applicationUserSerivce.GetUserByEmailAsync(email);
            if (user == null) return NotFound("Cannot find user with that email");
            return Ok(await _applicationUserSerivce.GetUserByEmailAsync(email));
        }


        [HttpGet]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return Ok(new { model });
        }


        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user != null)
            {
                var resetPasswordResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
                if (!resetPasswordResult.Succeeded)
                {
                    foreach (var error in resetPasswordResult.Errors)
                    {

                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }
                return StatusCode(StatusCodes.Status200OK, new ResponseDto { Success = true, Message = $"Password has been changed" });

            }
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto { Success = true, Message = $"Could not send link to email" });
        }


    }
}
