using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.DataModel;
using OneTrack_v2.Services;
using OneTrak_v2.Services;

namespace OneTrack_v2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #region "Admin Display"
        [HttpGet]
        public async Task<IActionResult> GetCompanyTypes()
        {
            var result = await Task.Run(() => _adminService.GetCompanyTypes());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{companyType}")]
        public async Task<IActionResult> GetCompaniesByType(string companyType)
        {
            var result = await Task.Run(() => _adminService.GetCompaniesByType(companyType));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLicenseTypes()
        {
            var result = await Task.Run(() => _adminService.GetLicenseTypes());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{state}/{licenesTypeID}")]
        public async Task<ActionResult> GetConEduLicenses(string state, int licenesTypeID)
        {
            var result = await Task.Run(() => _adminService.GetConEduLicenses(state, licenesTypeID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetCompanyRequirements(string workState, string? resState = null)
        {
            var result = await Task.Run(() => _adminService.GetCompanyRequirements(workState, resState));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetDropdownListTypes()
        {
            var result = await Task.Run(() => _adminService.GetDropdownListTypes());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{lkpField}")]
        public async Task<IActionResult> GetDropdownByType(string lkpField)
        {
            var result = await Task.Run(() => _adminService.GetDropdownByType(lkpField));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{state}")]
        public async Task<IActionResult> GetExamByState(string state)
        {
            var result = await Task.Run(() => _adminService.GetExamByState(state));

            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetJobTitleLicLevel()
        {
            var result = await Task.Run(() => _adminService.GetJobTitleLicLevel());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetJobTitlelicIncentive()
        {
            var result = await Task.Run(() => _adminService.GetJobTitlelicIncentive());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetJobTitlelicensed()
        {
            var result = await Task.Run(() => _adminService.GetJobTitlelicensed());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLicenseEditList()
        {
            var result = await Task.Run(() => _adminService.GetLicenseEditList());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{licenseID}")]
        public async Task<IActionResult> GetLicenseEditByID(int licenseID)
        {
            var result = await Task.Run(() => _adminService.GetLicenseEditByID(licenseID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLicTechList()
        {
            var result = await Task.Run(() => _adminService.GetLicTechList());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{state}")]
        public async Task<IActionResult> GetPreEduEditByState(string state)
        {
            var result = await Task.Run(() => _adminService.GetPreEduEditByState(state));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductEditList()
        {
            var result = await Task.Run(() => _adminService.GetProductEditList());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{workState}/{resState}")]
        public async Task<ActionResult> GetStateLicRequirementList(string workState, string resState)
        {
            var result = await Task.Run(() => _adminService.GetStateLicRequirementList(workState, resState));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetStateProvinceList()
        {
            var result = await Task.Run(() => _adminService.GetStateProvinceList());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetXBorderBranchList()
        {
            var result = await Task.Run(() => _adminService.GetXBorderBranchList());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{branchCode}")]
        public async Task<IActionResult> GetXBorderBranchByCode(int branchCode)
        {
            var result = await Task.Run(() => _adminService.GetXBorderBranchByCode(branchCode));

            return StatusCode(result.StatusCode, result);
        }
        #endregion
    }
}
