using FlightDocs.Serivce.DocumentApi.Data;
using FlightDocs.Service.DocumentApi.Models;
using FlightDocs.Service.DocumentApi.Models.Dto;
using FlightDocs.Service.DocumentApi.Service;
using FlightDocs.Service.DocumentApi.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace FlightDocs.Service.DocumentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IDocumentService _documentService;


        public DocumentApiController(IFlightService flightService, ITimeService timeService, IDocumentService documentService)
        {

            _flightService = flightService;
            _documentService = documentService;


        }

        [HttpGet("{flightId}")]
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


        //[HttpGet("Test/Download")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var d = await _dbContext.Documents.Where(d => d.Id == id).FirstOrDefaultAsync();
        //    if (d != null)
        //    {
        //        var fileContent = d.FileData;
        //        if (fileContent == null || fileContent.Length == 0)
        //        {
        //            return NotFound("File content not found");
        //        }
        //        return File(fileContent, d.FileType, d.FileName);
        //    }
        //    return NotFound("SDF");
        //}


        //[HttpPost("")]
        ////[Authorize(Roles = "ADMIN")]
        //public async Task<IActionResult> CreateDocumentForFlight(string flightId, [FromForm] DocumentUploadModel model)
        //{
        //    var flighExist = await _flightService.GetFlightByIdAsync(flightId);
        //    if (flighExist == null)
        //    {
        //        return BadRequest("Cannot find that flight");
        //    }
        //    // valid date of flight 
        //    if (flighExist.DepartureDateTime >= _timeService.GetCurrentTimeInVietnam())
        //    {
        //        return BadRequest("Cannot add document cause that flight is ending");
        //    }

        //    //test
        //    var document = new Document
        //    {
        //        FileName = model.File.FileName,
        //        FileType = model.File.ContentType,
        //        // Other document properties
        //        FlightId = flightId,
        //        CreateBy = "TEST",
        //        DocumentTypeId = 1,CreateDate = DateTime.Now

        //    };

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        await model.File.CopyToAsync(memoryStream);
        //        document.FileData = memoryStream.ToArray();
        //    }
        //    await _dbContext.Set<Document>().AddAsync(document);
        //    await _dbContext.SaveChangesAsync(); // Save changes to get the DocumentType.Id

        //    return Ok("Document added to flight successfully!");

        //    //test

        //    return Ok(flighExist);
        //}
    }
}
