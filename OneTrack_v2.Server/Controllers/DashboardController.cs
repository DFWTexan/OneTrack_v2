using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.Services;
using OneTrak_v2.Services;

namespace OneTrak_v2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboard)
        {
            _dashboardService = dashboard;
        }

        [HttpGet]
        public async Task<ActionResult> GetAuditModifiedBy()
        {
            var result = await Task.Run(() => _dashboardService.GetAuditModifiedBy());

            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        public async Task<ActionResult> GetAuditLog(DateTime startDate, DateTime endDate, string? modifiedBy = null)
        {
            var result = await Task.Run(() => _dashboardService.GetAuditLog(startDate, endDate, modifiedBy));

            return StatusCode(result.StatusCode, result);
        }

    }
}
