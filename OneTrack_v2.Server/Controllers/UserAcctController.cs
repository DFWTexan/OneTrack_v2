using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneTrak_v2.Services;

namespace OneTrak_v2.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAcctController : ControllerBase
    {
        private readonly ILdapService  _ldapService;

        public UserAcctController(ILdapService ldapService)
        {
            _ldapService = ldapService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAccount(string UserName, string PassWord)
        {
            var result = await Task.Run(() => _ldapService.GetUserAccount(UserName, PassWord));

            return StatusCode(result.StatusCode, result);
        }

    }
}
