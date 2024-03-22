using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.Services;
using OneTrak_v2.Services;

namespace OneTrak_v2.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TicklerMgmtController : ControllerBase
    {
        private readonly ITicklerMgmt _ticklerMgmt;

        public TicklerMgmtController(ITicklerMgmt ticklerMgmt)
        {
            _ticklerMgmt = ticklerMgmt;
        }

        [HttpGet]
        public async Task<ActionResult> GetIncentiveRolloutGroups(int ticklerID, int licenseTechID, int employmentID)
        {
            var result = await Task.Run(() => _ticklerMgmt.GetTicklerInfo(ticklerID, licenseTechID, employmentID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetStockTickler()
        {
            var result = await Task.Run(() => _ticklerMgmt.GetStockTickler());

            return StatusCode(result.StatusCode, result);
        }
    }
}
