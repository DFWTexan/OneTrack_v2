using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.Services;
using OneTrak_v2.DataModel;
using OneTrak_v2.Services;

namespace OneTrak_v2.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TicklerMgmtController : ControllerBase
    {
        private readonly ITicklerMgmtService _ticklerMgmt;

        public TicklerMgmtController(ITicklerMgmtService ticklerMgmt)
        {
            _ticklerMgmt = ticklerMgmt;
        }

        [HttpGet]
        public async Task<ActionResult> GetTicklerInfo(int ticklerID, int licenseTechID, int employmentID)
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

        [HttpGet]
        public async Task<ActionResult> GetLicenseTech(int licenseTechID, string? soeid)
        {
            var result = await Task.Run(() => _ticklerMgmt.GetLicenseTech(licenseTechID, soeid));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> UpsertTickler([FromBody] IputUpsertTicklerMgmt input)
        {
            var result = await Task.Run(() => _ticklerMgmt.UpsertTickler(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> CloseTickler([FromBody] IputCloseTicklerMgmt input)
        {
            var result = await Task.Run(() => _ticklerMgmt.CloseTickler(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> DeleteTickler([FromBody] IputDeleteTicklerMgmt input)
        {
            var result = await Task.Run(() => _ticklerMgmt.DeleteTickler(input));

            return StatusCode(result.StatusCode, result);
        }
    }
}
