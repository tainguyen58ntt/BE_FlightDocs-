using FlightDocs.Service.FlightAPI.Models;
using FlightDocs.Service.FlightAPI.Models.Dto;
using FlightDocs.Service.FlightAPI.Pagination;
using FlightDocs.Service.FlightAPI.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs.Service.FlightAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlighApiController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlighApiController(IFlightService flightService)
        {
            _flightService = flightService;
           

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlightById(string id)
        {
            return Ok(await _flightService.GetFlightByIdAsync(id));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPageAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var result = await _flightService.GetPaginationAsync(pageIndex, pageSize);
            return Ok(result);

        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateAsync([FromBody] FlightRequestDto flightRequestDto)
        {


            return Ok();
        }
    }
}
