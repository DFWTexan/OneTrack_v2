using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneTrak_v2.Services;

namespace OneTrak_v2.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorklistController : ControllerBase
    {
        private readonly IWorklistService _worklistService;

        public WorklistController(IWorklistService worklistService)
        {
            _worklistService = worklistService;
        }

        [HttpGet]
        public async Task<ActionResult> GetWorklistData(string? worklistName = null, string? worklistDate = null, string? licenseTech = null)
        {
            var result = await Task.Run(() => _worklistService.GetWorklistData(worklistName, worklistDate, licenseTech));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetWorklistDataByLicenseTech(string licenseTech)
        {
            var result = await Task.Run(() => _worklistService.GetWorklistData(licenseTech));

            return StatusCode(result.StatusCode, result);
        }
    }
}
