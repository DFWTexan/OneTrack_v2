using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.Services;
using OneTrak_v2.DataModel;

namespace OneTrak_v2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult> GetEmailComTemplates()
        {
            var result = await Task.Run(() => _emailService.GetEmailComTemplates());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{communicationID}/{employmentID}")]
        public async Task<ActionResult> GetEmailTemplate(int communicationID, int employmentID)
        {
            var result = await Task.Run(() => _emailService.GetEmailTemplate(communicationID, employmentID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> Send([FromForm] IputSendEmail input)
        {
            if (input.Attachment != null)
            {
                try
                {
                    // Here you would handle the file, such as saving it or attaching it to the email
                    // This step depends on how your IEmailService is implemented
                    // For example, to read the file as a byte array:
                    using (var memoryStream = new MemoryStream())
                    {
                        await input.Attachment.CopyToAsync(memoryStream);
                        byte[] fileBytes = memoryStream.ToArray();
                        // Handle the byte array as needed
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest($"An error occurred while processing the file: {ex.Message}");
                }
            }

            // Adjust the call to _emailService.Send as needed to handle the potentially changed input structure
            var result = await Task.Run(() => _emailService.Send(input));

            return StatusCode(result.StatusCode, result);
        }
    }
}
