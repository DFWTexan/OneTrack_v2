using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.DataModel;
using OneTrack_v2.Services;

namespace OneTrack_v2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MiscController : ControllerBase
    {
        private readonly IMiscService _miscService;

        public MiscController(IMiscService miscService)
        {
            _miscService = miscService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStateProvince()
        {
            var result = await Task.Run(() => _miscService.GetStateProvinces());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBranches()
        {
            var result = await Task.Run(() => _miscService.GetBranches());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetScoreNumbers()
        {
            var result = await Task.Run(() => _miscService.GetScoreNumbers());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployerAgencies()
        {
            var result = await Task.Run(() => _miscService.GetEmployerAgencies());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLicenseStatuses()
        {
            var result = await Task.Run(() => _miscService.GetLicenseStatuses());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLicenseNames()
        {
            var result = await Task.Run(() => _miscService.GetLicenseNames());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{stateAbv}")]
        public async Task<IActionResult> GetLicenseNumericNames(string stateAbv)
        {
            var result = await Task.Run(() => _miscService.GetLicenseNumericNames(stateAbv));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmailTemplates()
        {
            var result = await Task.Run(() => _miscService.GetEmailTemplates());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTicklerMessageTypes()
        {
            var result = await Task.Run(() => _miscService.GetTicklerMessageTypes());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetLicenseTeches()
        {
            var result = await Task.Run(() => _miscService.GetLicenseTeches());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> WorkListNames()
        {
            var result = await Task.Run(() => _miscService.WorkListNames());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetBackgroundStatuses()
        {
            var result = await Task.Run(() => _miscService.GetBackgroundStatuses());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetJobTitles()
        {
            var result = await Task.Run(() => _miscService.GetJobTitles());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{licenseID}")]
        public async Task<IActionResult> GetCoAbvByLicenseID(int licenseID)
        {
            var result = await Task.Run(() => _miscService.GetCoAbvByLicenseID(licenseID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{stateAbv}")]
        public async Task<IActionResult> GetPreEducationByStateAbv(string stateAbv)
        {
            var result = await Task.Run(() => _miscService.GetPreEducationByStateAbv(stateAbv));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{stateAbv}")]
        public async Task<IActionResult> GetPreExamByStateAbv(string stateAbv)
        {
            var result = await Task.Run(() => _miscService.GetPreExamByStateAbv(stateAbv));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAgentStautes()
        {
            var result = await Task.Run(() => _miscService.GetAgentStautes());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAppointmentStatuses()
        {
            var result = await Task.Run(() => _miscService.GetAppointmentStatuses());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetApplicationsStatuses()
        {
            var result = await Task.Run(() => _miscService.GetApplicationsStatuses());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetPreEducationStatuses()
        {
            var result = await Task.Run(() => _miscService.GetPreEducationStatuses());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetPreExamStatuses()
        {
            var result = await Task.Run(() => _miscService.GetPreExamStatuses());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetRenewalMethods()
        {
            var result = await Task.Run(() => _miscService.GetRenewalMethods());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetRollOutGroups()
        {
            var result = await Task.Run(() => _miscService.GetRollOutGroups());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{usageType}")]
        public async Task<IActionResult> GetContEduInfo(string usageType)
        {
            var result = await Task.Run(() => _miscService.GetContEduInfo(usageType));

            return StatusCode(result.StatusCode, result);
        }
    }
}
