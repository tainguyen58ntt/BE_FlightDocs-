using AutoMapper;
using FlightDocs.Serivce.DocumentApi.Data;
using FlightDocs.Service.DocumentApi.Models;
using FlightDocs.Service.DocumentApi.Models.Dto;
using FlightDocs.Service.DocumentApi.Service;
using FlightDocs.Service.DocumentApi.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;
using Document = FlightDocs.Service.DocumentApi.Models.Document;

namespace FlightDocs.Service.DocumentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IDocumentService _documentService;
        private readonly ITimeService _timeService;
        private readonly IDocumentTypeService _documentTypeService;



        public DocumentApiController(IDocumentTypeService documentTypeService, IFlightService flightService, ITimeService timeService, IDocumentService documentService)
        {

            _flightService = flightService;
            _documentService = documentService;
            _timeService = timeService;
            _documentTypeService = documentTypeService;
           

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPageAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest("Page index cannot be negative");
            if (pageSize <= 0) return BadRequest("Page size must greater than 0");
            var result = await _documentService.GetPaginationAsync(pageIndex, pageSize);
            return Ok(result);

        }

        [HttpGet("{flightId}")]
        [Authorize(Roles = "ADMIN, CREW, PILOT")]
        public async Task<IActionResult> Get(string flightId)
        {
            var flight = await _flightService.GetFlightByIdAsync(flightId);
            if (flight == null)
            {
                return BadRequest("Cannot find that flight");
            }

            var documentByFlightId = await _documentService.GetDocumentByFlightIdAsync(flightId);
            if (documentByFlightId == null)
            {
                return NoContent();
            }
            return Ok(await _documentService.GetDocumentByFlightIdAsync(flightId));


        }


        [HttpGet("{flightId}/updated-by-user")]
        [Authorize(Roles = "ADMIN, CREW, PILOT")]
        public async Task<IActionResult> GetDocumentWasUpdatedByUser(string flightId)
        {
            var flight = await _flightService.GetFlightByIdAsync(flightId);
            if (flight == null)
            {
                return BadRequest("Cannot find that flight");
            }

            var documentByFlightId = await _documentService.GetDocumentWasUpdatedByUser(flightId);
          
            return Ok(await _documentService.GetDocumentWasUpdatedByUser(flightId));


        }






        [HttpGet("{documentId}/download")]
        public async Task<IActionResult> DownloadDocumentId(int documentId)
        {
            var documentResponseDto = await _documentService.GetDocumentByIdAsync(documentId);
            if (documentResponseDto == null)
            {
                return BadRequest("Cannot find that document");
            }

            var fileContent = documentResponseDto.FileData;

            if (fileContent == null || fileContent.Length == 0)
            {
                return NotFound("File content not found");
            }

            return File(fileContent, documentResponseDto.FileType, documentResponseDto.FileName);


        }









        [HttpPost("{flightId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateDocumentForFlight(string flightId, [FromForm] DocumentUploadModel model, int documentTypeId)
        {
            var flighExist = await _flightService.GetFlightByIdAsync(flightId);
            if (flighExist == null)
            {
                return BadRequest("Cannot find that flight");
            }
            // valid date of flight 
            if (flighExist.DepartureDateTime >= _timeService.GetCurrentTimeInVietnam())
            {
                return BadRequest("Cannot add document cause that flight is ending");
            }

            // 
            var documentType = await _documentTypeService.GetDocumentTypeByIdAsync(documentTypeId);
            if (documentType == null)
            {
                return BadRequest("Cannot find that document type");
            }

            bool isCreated = await _documentService.CreateDocumentForFlight(flightId, model, documentTypeId);
            if (isCreated == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Create Document Fail. Error server" });
            }



            return Ok();
        }
    }
}
