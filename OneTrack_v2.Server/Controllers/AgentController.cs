using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.DataModel;
using OneTrack_v2.Services;
using OneTrak_v2.DataModel;

namespace OneTrack_v2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _agentService;

        //private Microsoft.Extensions.Configuration.IConfiguration _config;
        //private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;
        //private Microsoft.Extensions.Logging.ILogger _logger;
        //private IHttpContextAccessor _httpContext;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }
        //public AgentController(IConfiguration configuration, IWebHostEnvironment environment, IAgentService agentService)
        //{
        //    _config = configuration;
        //    _env = environment;
        //    _agentService = agentService;
        //    //_logger = logger;
        //    //_httpContext = httpContextAccessor;
        //}

        #region "Agent Display"
        [HttpGet("{employeeID}")]
        public async Task<IActionResult> GetAgentByEmployeeID(int employeeID)
        {
            var result = await Task.Run(() => _agentService.GetAgentByEmployeeID(employeeID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{employmentID}")]
        public async Task<ActionResult> GetLicenses(int employmentID)
        {
            var result = await Task.Run(() => _agentService.GetLicenses(employmentID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{employmentID}")]
        public async Task<ActionResult> GetAppointments(int employmentID)
        {
            var result = await Task.Run(() => _agentService.GetAppointments(employmentID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{employmentID}")]
        public async Task<ActionResult> GetLicenseAppointments(int employmentID)
        {
            var result = await Task.Run(() => _agentService.GetLicenseAppointments(employmentID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> GetEmploymentTransferHistory([FromBody] IputAgentEmploymentTransferHistory Input)
        {
            var result = await Task.Run(() => _agentService.GetEmploymentTransferHistory(Input.EmploymentID, Input.EmploymentHistoryID, Input.TransferHistoryID, Input.EmploymentJobTitleID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{employmentID}")]
        public async Task<ActionResult> GetContEducationRequired(int employmentID)
        {
            var result = await Task.Run(() => _agentService.GetContEducationRequired(employmentID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{employmentID}")]
        public async Task<ActionResult> GetDiary(int employmentID)
        {
            var result = await Task.Run(() => _agentService.GetDiary(employmentID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{employmentID}")]
        public async Task<ActionResult> GetCommunications(int employmentID)
        {
            var result = await Task.Run(() => _agentService.GetCommunications(employmentID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{employeeLicenseID}")]
        public async Task<ActionResult> GetLicenseApplcationInfo(int employeeLicenseID)
        {
            var result = await Task.Run(() => _agentService.GetLicenseApplcationInfo(employeeLicenseID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetBranchCodes()
        {
            var result = await Task.Run(() => _agentService.GetBranchCodes());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetCoRequirementAssetIDs()
        {
            var result = await Task.Run(() => _agentService.GetCoRequirementAssetIDs());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetCoRequirementStatuses()
        {
            var result = await Task.Run(() => _agentService.GetCoRequirementStatuses());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetLicLevels()
        {
            var result = await Task.Run(() => _agentService.GetLicLevels());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetLicIncentives()
        {
            var result = await Task.Run(() => _agentService.GetLicIncentives());

            return StatusCode(result.StatusCode, result);
        }
        #endregion

        #region "Agent INSERT"
        [HttpPost]
        public async Task<ActionResult> UpsertAgent([FromBody] IputUpsertAgent input)
        {
            var result = await Task.Run(() => _agentService.UpsertAgent(input));

            return StatusCode(result.StatusCode, result);
        }

        //[HttpPost]
        //public async Task<ActionResult> InsertAgent_v2([FromBody] IputUpsertAgent Input)
        //{
        //    var result = await Task.Run(() => _agentService.InsertAgent_v2(Input));  // Implimentation of InsertAgent_v2 Linq Query

        //    return StatusCode(result.StatusCode, result);
        //}
        #endregion

        #region "Agent Update"
        [HttpPost]
        public async Task<ActionResult> UpdateAgentDetails([FromBody] IputAgentDetail input)
        {
            var result = await Task.Run(() => _agentService.UpdateAgentDetails(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> UpsertEmploymentHistItem([FromBody] InputEmploymentHistItem input)
        {
            var result = await Task.Run(() => _agentService.UpsertEmploymentHistItem(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> UpsertTranserHistItem([FromBody] IputTransferHistoryItem input)
        {
            var result = await Task.Run(() => _agentService.UpsertTranserHistItem(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> UpsertCoRequirementItem([FromBody] IputCoRequirementItem input)
        {
            var result = await Task.Run(() => _agentService.UpsertCoRequirementItem(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> UpsertEmploymentJobTitleItem([FromBody] IputEmploymentJobTitleItem input)
        {
            var result = await Task.Run(() => _agentService.UpsertEmploymentJobTitleItem(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> UpsertAgentLicense([FromBody] IputUpsertAgentLicense input)
        {
            var result = await Task.Run(() => _agentService.UpsertAgentLicense(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> UpsertConEduTaken([FromBody] IputUpsertConEduTaken input)
        {
            var result = await Task.Run(() => _agentService.UpsertConEduTaken(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> UpsertDiaryItem([FromBody] IputUpsertDiaryItem input)
        {
            var result = await Task.Run(() => _agentService.UpsertDiaryItem(input));

            return StatusCode(result.StatusCode, result);
        }
        #endregion

        #region "Agent Delete"
        [HttpPost]
        public async Task<ActionResult> DeleteEmploymentHistItem([FromBody] IputDeleteEmploymentHistoryItem input)
        {
            var result = await Task.Run(() => _agentService.DeleteEmploymentHistItem(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAgentEmployee([FromBody] IputDeleteEmployee input)
        {
            var result = await Task.Run(() => _agentService.DeleteAgentEmployee(input.EmployeeID, input.UserSOEID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteTransferHistItem([FromBody] IputDeleteTransferHisttoryItem input)
        {
            var result = await Task.Run(() => _agentService.DeleteTransferHistItem(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCoRequirementItem([FromBody] IputDeleteCoRequirementItem input)
        {
            var result = await Task.Run(() => _agentService.DeleteCoRequirementItem(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteEmploymentJobTitleItem([FromBody] IputDeleteEmploymentJobTitle input)
        {
            var result = await Task.Run(() => _agentService.DeleteEmploymentJobTitleItem(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAgentLicense([FromBody] IputDeleteAgentLincense input)
        {
            var result = await Task.Run(() => _agentService.DeleteAgentLicense(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteConEduTaken([FromBody] IputDeleteConEduTaken input)
        {
            var result = await Task.Run(() => _agentService.DeleteConEduTaken(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteDiaryItem([FromBody] IputDeleteDiaryItem input)
        {
            var result = await Task.Run(() => _agentService.DeleteDiaryItem(input));

            return StatusCode(result.StatusCode, result);
        }
        #endregion
    }
}
