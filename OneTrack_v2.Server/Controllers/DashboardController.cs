using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.Services;
using OneTrak_v2.DataModel;
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
        public async Task<ActionResult> GetAdBankerIncompleteCount()
        {
            var result = await Task.Run(() => _dashboardService.GetAdBankerIncompleteCount());

            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        public async Task<ActionResult> GetAdBankerImportStatus()
        {
            var result = await Task.Run(() => _dashboardService.GetAdBankerImportStatus());

            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        public async Task<ActionResult> GetAdBankerImportData(DateTime startDate, DateTime endDate, string importStatus)
        {
            var result = await Task.Run(() => _dashboardService.GetAdBankerImportData(startDate, endDate, importStatus == "All" ? null : importStatus == "Success" ? true : false));

            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        public async Task<ActionResult> CompleteImportStatus([FromBody] IputADBankerImportStatus input)
        {
            var result = await Task.Run(() => _dashboardService.CompleteImportStatus(input));

            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        public async Task<ActionResult> GetAuditModifiedBy(bool isActive)
        {
            var result = await Task.Run(() => _dashboardService.GetAuditModifiedBy(isActive));

            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        public async Task<ActionResult> GetAuditLog(DateTime startDate, DateTime endDate, string? modifiedBy = null)
        {
            var result = await Task.Run(() => _dashboardService.GetAuditLog(startDate, endDate, modifiedBy));

            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        public async Task<ActionResult> GetAuditLogAdHoc(DateTime startDate, DateTime endDate, string? modifiedBy = null, string? baseTableName = null, string? baseTableKeyValue = null, string? auditFieldName = null, string? auditAction = null)
        {
            var result = await Task.Run(() => _dashboardService.GetAuditLogAdHoc(startDate, endDate, modifiedBy = null, baseTableName = null, baseTableKeyValue = null, auditFieldName = null, auditAction = null));

            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        public async Task<ActionResult> GetAuditBaseTableNames()
        {
            var result = await _dashboardService.GetAuditBaseTableNames();

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{memberID}")]
        public async Task<ActionResult> GetEmployeeIdWithTMemberID(string memberID)
        {
            var result = await Task.Run(() => _dashboardService.GetEmployeeIdWithTMemberID(memberID));

            return StatusCode(result.StatusCode, result);
        }
    }
}
