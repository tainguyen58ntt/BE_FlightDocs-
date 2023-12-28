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
        public async Task<IActionResult> Get() {
            return Ok(await _applicationUserService.GetUserById("6c7278ac-d759-4de0-a09f-5e0325a357a6"));
        
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(int id)
        {
            return Ok(await _groupService.GetGroupByIdAsync(id));
        }

        // list all group 
        [HttpGet]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetPageAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var result = await _groupService.GetPaginationAsync(pageIndex, pageSize);
            return Ok(result);

        }


        [HttpGet("get-group-by-name")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetGroupByName([FromQuery] string name)
        {
        
            var result = await _groupService.GetGroupByNameAsync(name);
            return Ok(result);

        }

        // create group
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateAsync([FromBody] GroupRequestDto vm)
        {
            var isCreated = await _groupService.CreatAsync(vm);
            if(isCreated == false) return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Create Fail. Error server" });
            
            return Ok();
        }

        // assign account to group
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AssignMemberToGroupAsync(int id, [FromQuery] string email)
        {
            // find group id 
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group == null) return NotFound($"Not found group with that id");
            // find email user 
            var user = await _applicationUserService.GetUserByEmail(email);
            if (user == null) return NotFound($"Not found user with that email");
            // // assign
            var isAssign = await _groupService.AssingMemeberToGroupAsync(id, email);
            if (isAssign == false) return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Assign Fail. Error server" });
            return Ok();
        }
    }
}
