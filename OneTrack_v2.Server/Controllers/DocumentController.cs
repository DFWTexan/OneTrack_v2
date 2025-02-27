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
        public async Task<ActionResult> Upload([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] string filePathType)
        {

            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            var stream = file.OpenReadStream();

            var result = await _documentService.Upload(stream, fileName, filePathType);

            return StatusCode(result.StatusCode, result);
        }

        //[HttpPost]
        //public async Task<ActionResult> Delete([FromBody] OneTrak_v2.Document.Model.IputFileUploadDelete input)
        //{
        //    if (string.IsNullOrEmpty(input.TargetPathUri) || string.IsNullOrEmpty(input.FileName))
        //        return BadRequest("File path or file name is empty");

        //    var result = await Task.Run(() => _documentService.Delete(input.TargetPathUri, input.FileName));

        //    return StatusCode(result.StatusCode, result);
        //}

        // GET api/file/{filename}
        [HttpGet]
        public async Task<IActionResult> GetFileContent(string path, string filename)
        {
            var filePath = Path.Combine(path, filename);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found");
            }

            try
            {
                var content = await System.IO.File.ReadAllTextAsync(filePath);
                return Ok(content);
            }
            catch (IOException ioEx)
            {
                return StatusCode(500, $"Error reading file: {ioEx.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetFile(string path, string filename)
        {
            var filePath = Path.Combine(path, filename);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found");
            }

            try
            {
                // Open the file stream
                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    stream.CopyTo(memory);
                }
                memory.Position = 0;

                // Determine the content type based on the file extension
                string contentType = GetContentType(filePath);

                // Return the file as a FileStreamResult
                return File(memory, contentType, filename);
            }
            catch (IOException ioEx)
            {
                return StatusCode(500, $"Error reading the file: {ioEx.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetFileDownload(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found");
            }

            try
            {
                // Extract the filename from the file path
                string fileName = Path.GetFileName(filePath);

                // Open the file stream
                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    stream.CopyTo(memory);
                }
                memory.Position = 0;

                // Determine the content type based on the file extension
                string contentType = GetContentType(filePath);

                // Return the file as a FileStreamResult
                return File(memory, contentType, fileName);
            }
            catch (IOException ioEx)
            {
                return StatusCode(500, $"Error reading the file: {ioEx.Message}");
            }
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            // Common MIME types
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                {".pdf", "application/pdf"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                // Add more MIME types as needed
            };
        }
    }
}
