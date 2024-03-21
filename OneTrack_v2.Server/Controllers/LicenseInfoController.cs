using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.Services;

namespace OneTrak_v2.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LicenseInfoController : ControllerBase
    {
        private readonly ILicenseInfo _licenseInfo;

        public LicenseInfoController(ILicenseInfo licenseInfo)
        {
            _licenseInfo = licenseInfo;
        }

        [HttpGet("{employeelicenseID}")]
        public async Task<ActionResult> GetIncentiveInfo(int employeelicenseID)
        {
            var result = await Task.Run(() => _licenseInfo.GetIncentiveInfo(employeelicenseID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetIncentiveRolloutGroups()
        {
            var result = await Task.Run(() => _licenseInfo.GetIncentiveRolloutGroups());

            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet]
        public async Task<ActionResult> GetIncentiveBMMgrs()
        {
            var result = await Task.Run(() => _licenseInfo.GetIncentiveBMMgrs());

            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet]
        public async Task<ActionResult> GetIncentiveDMMrgs()
        {
            var result = await Task.Run(() => _licenseInfo.GetIncentiveDMMrgs());

            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet]
        public async Task<ActionResult> GetIncentiveTechNames()
        {
            var result = await Task.Run(() => _licenseInfo.GetIncentiveTechNames());

            return StatusCode(result.StatusCode, result);
        }
    }
}
