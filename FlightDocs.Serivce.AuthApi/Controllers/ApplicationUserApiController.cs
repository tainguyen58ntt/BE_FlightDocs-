using FlightDocs.Serivce.AuthApi.Service.IService;
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
    }
}
