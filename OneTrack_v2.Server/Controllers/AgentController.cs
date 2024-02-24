﻿using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.DataModel;
using OneTrack_v2.Services;

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
        #endregion

        #region "Agent Insert"
        [HttpPost]
        public async Task<ActionResult> InsertAgent([FromBody] IputAgentInsert Input)
        {
            var result = await Task.Run(() => _agentService.InsertAgent(Input));  // Implimentation of InsertAgent SQL Storre Procedure

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]  
        public async Task<ActionResult> InsertAgent_v2([FromBody] IputAgentInsert Input)
        {
            var result = await Task.Run(() => _agentService.InsertAgent_v2(Input));  // Implimentation of InsertAgent_v2 Linq Query

            return StatusCode(result.StatusCode, result);
        }
        #endregion

        #region "Agent Update"
        #endregion

        #region "Agent Delete"
        #endregion
    }
}