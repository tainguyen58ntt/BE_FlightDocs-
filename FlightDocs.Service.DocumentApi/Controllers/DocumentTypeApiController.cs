using FlightDocs.Service.DocumentApi.Models.Dto;
using FlightDocs.Service.DocumentApi.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs.Service.DocumentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeApiController : ControllerBase
    {
        private readonly IDocumentTypeService _documentTypeService;
        private readonly IFlightService _flightService;
        public DocumentTypeApiController(IDocumentTypeService documentTypeService, IFlightService flightService)
        {
            _documentTypeService = documentTypeService; 
            _flightService  = flightService;
        }

        [HttpPost("")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateDocumentType(DocumentTypeRequest documentTypeRequest)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = string.Join(", ", ModelState.Values
                                            .SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage))
                };

                return BadRequest(errorResponse);
            }
            return Ok(await _documentTypeService.CreatAsync(documentTypeRequest));
        }


        [HttpGet]
        //[Authorize(Roles = "ADMIN")]
        [Authorize]
        public async Task<IActionResult> GetPageAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var result = await _documentTypeService.GetPaginationAsync(pageIndex, pageSize);
            return Ok(result);

        }

    }
}
