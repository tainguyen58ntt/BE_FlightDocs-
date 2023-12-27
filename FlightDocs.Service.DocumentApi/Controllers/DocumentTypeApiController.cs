using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs.Service.DocumentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeApiController : ControllerBase
    {
        

        //[HttpPost]
        ////[Authorize(Roles = "ADMIN")]
        //public async Task<IActionResult> CreateDocumentType()
        //{
        //    return Ok(await _applicationUserSerivce.GetUserAsync());
        //}

    }
}
