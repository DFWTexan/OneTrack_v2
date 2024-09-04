using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.Services;
using OneTrak_v2.Services;

namespace OneTrak_v2.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAcctController : ControllerBase
    {
        private readonly ILdapService  _ldapService;
        private readonly IMiscService _miscService;

        public UserAcctController(ILdapService ldapService, IMiscService miscService)
        {
            _ldapService = ldapService;
            _miscService = miscService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAcctInfo(string UserName, string PassWord)
        {
            var result = await Task.Run(() => _ldapService.GetUserAcctInfo(UserName, PassWord));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{licenseTechID}")]
        public async Task<IActionResult> GetLicenseTechByID(int licenseTechID)
        {
            var result = await Task.Run(() => _miscService.GetLicenseTechByID(licenseTechID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{soeID}")]
        public async Task<IActionResult> GetLicenseTechBySOEID(string soeID)
        {
            var result = await Task.Run(() => _miscService.GetLicenseTechBySOEID(soeID));

            return StatusCode(result.StatusCode, result);
        }

    }
}
