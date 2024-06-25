using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.DataModel;
using OneTrack_v2.Services;
using OneTrak_v2.Services;

namespace OneTrak_v2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost]
        public async Task<ActionResult> Upload([FromForm] OneTrak_v2.Document.Model.IputFileUpload input)
        {

            if (input.File == null || input.File.Length == 0)
                return BadRequest("File is empty");

            string fileBytes = string.Empty;

            string fileName = input.File.FileName;

            var stream = input.File.OpenReadStream();

            var result = await Task.Run(() => _documentService.Upload(input.FilePathUri, stream));

            return StatusCode(result.StatusCode, result);
        }
    }
}
