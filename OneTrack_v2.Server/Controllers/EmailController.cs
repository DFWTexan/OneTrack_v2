using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.Services;

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
    }
}
