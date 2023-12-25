using FlightDocs.Serivce.AuthApi.Models;
using FlightDocs.Serivce.AuthApi.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs.Serivce.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IApplicationUserSerivce _applicationUserSerivce;
      
        public AdminApiController(IApplicationUserSerivce applicationUserSerivce)
        {
            _applicationUserSerivce = applicationUserSerivce;
       


        }

        [HttpDelete("lock-user-by-email")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> LockUserByEmail(string email)
        {
            var user = await _applicationUserSerivce.GetUserByEmailAsync(email);
            if (user == null) return NotFound("Cannot find user with that email");
            if (user != null)
            {
               var terminalUser =  await _applicationUserSerivce.TerminalUserByEmailAsync(email);
                if(!terminalUser) return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Terminal User Fail. Error server" });
            }

            return Ok();
        }


        [HttpPut("unlock-user-by-email")]
        //[Authorize(Roles = "ADMIN")]
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
    }
}
