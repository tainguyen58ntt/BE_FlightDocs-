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
            return Ok(await _documentTypeService.CreatAsync(documentTypeRequest));
        }

      

    }
}
