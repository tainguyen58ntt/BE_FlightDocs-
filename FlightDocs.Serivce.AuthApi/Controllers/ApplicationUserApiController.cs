using FlightDocs.Serivce.AuthApi.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs.Serivce.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserApiController : ControllerBase
    {
        private readonly IApplicationUserSerivce _applicationUserSerivce;


        public ApplicationUserApiController(IApplicationUserSerivce applicationUserSerivce)
        {
            _applicationUserSerivce = applicationUserSerivce;


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

   



    }
}
