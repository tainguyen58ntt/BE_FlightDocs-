using FlightDocs.Service.GroupApi.Models.Dto;
using FlightDocs.Service.GroupApi.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs.Service.GroupApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupApiController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IApplicationUserService _applicationUserService;

        public GroupApiController(IGroupService groupService, IApplicationUserService applicationUserService)
        {
            _groupService = groupService;
            _applicationUserService = applicationUserService;


        }

        //test
        [HttpGet("Test")]
        public IActionResult Get() {
            return Ok(_applicationUserService.GetUserNameById("test"));
        
        }

        // list all group 
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetPageAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var result = await _groupService.GetPaginationAsync(pageIndex, pageSize);
            return Ok(result);

        }

        // create group
        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateAsync([FromBody] GroupRequestDto vm)
        {
            var existProductCart = await _groupService.CreatAsync(vm);

            return Ok();
        }
            
        // assign account to group

            
    }
}
