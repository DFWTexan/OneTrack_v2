using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using OneTrack_v2.DataModel;
using OneTrack_v2.Services;
using OneTrak_v2.DataModel;
using OneTrak_v2.Server.DbData.DataModel.Admin;
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
        public async Task<IActionResult> GetLicenseTypes(string? vStateAbv = null)
        {
            var result = await Task.Run(() => _adminService.GetLicenseTypes(vStateAbv));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetConEducationRules(string? stateAbv = null, string? licenseType = null)
        {
            var result = await Task.Run(() => _adminService.GetConEducationRules(stateAbv, licenseType));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetConEducationRulesVsp(string? stateAbv = null, string? licenseType = null)
        {
            var result = await Task.Run(() => _adminService.GetConEducationRules_sp(stateAbv, licenseType));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetCompanyRequirements(string workState, string? resState = null)
        {
            var result = await Task.Run(() => _adminService.GetCompanyRequirementsAsync(workState, resState));

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
        public async Task<IActionResult> GetJobTitles()
        {
            var result = await Task.Run(() => _adminService.GetJobTitles());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{stateProv?}")]
        public async Task<IActionResult> GetLicenseByStateProv(string? stateProv = null)
        {
            var result = await Task.Run(() => _adminService.GetLicenseByStateProv(stateProv));

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
        public async Task<IActionResult> GetProductEdits()
        {
            var result = await Task.Run(() => _adminService.GetProductEdits());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetStateLicRequirements(string? workState = null, string? resState = null, string? branchCode = null)
        {
            var result = await Task.Run(() => _adminService.GetStateLicRequirements(workState, resState, branchCode));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetStateProvinceList()
        {
            var result = await Task.Run(() => _adminService.GetStateProvinceList());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetXBorderBranchCodes()
        {
            var result = await Task.Run(() => _adminService.GetXBorderBranchCodes());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{branchCode}")]
        public async Task<ActionResult> GetXBorLicRequirements(string branchCode)
        {
            var result = await Task.Run(() => _adminService.GetXBorLicRequirements(branchCode));

            return StatusCode(result.StatusCode, result);
        }

        #endregion

        #region "Admin Edit"
        [HttpPost]
        public async Task<IActionResult> UpsertCompany([FromBody] IputUpsertCompany company)
        {
            var result = await Task.Run(() => _adminService.UpsertCompany(company));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> DeleteCompany([FromBody] IputDeleteCompany input)
        {
            var result = await Task.Run(() => _adminService.DeleteCompany(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertCompanyRequirement([FromBody] IputUpsertCompanyRequirement companyRequirement)
        {
            var result = await Task.Run(() => _adminService.UpsertCompanyRequirement(companyRequirement));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> DeleteCompanyRequirement([FromBody] IputDeleteCompanyRequirement input)
        {
            var result = await Task.Run(() => _adminService.DeleteCompanyRequirement(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertEducationRule([FromBody] IputUpsertEducationRule input)
        {
            var result = await Task.Run(() => _adminService.UpsertEducationRule(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> DisableEducationRule([FromBody] IputDisableEducationRule input)
        {
            var result = await Task.Run(() => _adminService.DisableEducationRule(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertLkpType([FromBody] IputUpsertLkpType input)
        {
            var result = await Task.Run(() => _adminService.UpsertLkpType(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> DeleteLkpType([FromBody] IputDeleteLkpType input)
        {
            var result = await Task.Run(() => _adminService.DeleteLkpType(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertExam([FromBody] IputUpsertExam input)
        {
            var result = await Task.Run(() => _adminService.UpsertExam(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> DeleteExam([FromBody] IputDeleteExam input)
        {
            var result = await Task.Run(() => _adminService.DeleteExam(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertJobTitle([FromBody] IputUpsertJobTitle input)
        {
            var result = await Task.Run(() => _adminService.UpsertJobTitle(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertLicense([FromBody] IputUpsertLicense input)
        {
            var result = await Task.Run(() => _adminService.UpsertLicense(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> DeleteLicense([FromBody] IputDeleteLicense input)
        {
            var result = await Task.Run(() => _adminService.DeleteLicense(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddLicenseCompany([FromBody] IputAddLicenseCompany input)
        {
            var result = await Task.Run(() => _adminService.AddLicenseCompany(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLicenseCompany([FromBody] IputUpdateLicenseCompany input)
        {
            var result = await Task.Run(() => _adminService.UpdateLicenseCompany(input));

            return StatusCode(result.StatusCode, result);
        }
        
        [HttpPut]
        public async Task<ActionResult> DeleteLicenseCompany([FromBody] IputDeleteLicenseCompany input)
        {
            var result = await Task.Run(() => _adminService.DeleteLicenseCompany(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddLicenseExam([FromBody] IputAddLicenseExam input)
        {
            var result = await Task.Run(() => _adminService.AddLicenseExam(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLicenseExam([FromBody] IputUpdateLicenseExam input)
        {
            var result = await Task.Run(() => _adminService.UpdateLicenseExam(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> DeleteLicenseExam([FromBody] IputDeleteLicenseExam input)
        {
            var result = await Task.Run(() => _adminService.DeleteLicenseExam(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddLicensePreEducation([FromBody] IputAddLicensePreEducation input)
        {
            var result = await Task.Run(() => _adminService.AddLicensePreEducation(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLicensePreEducation([FromBody] IputUpdateLicensePreEducation input)
        {
            var result = await Task.Run(() => _adminService.UpdateLicensePreEducation(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> DeleteLicensePreEducation([FromBody] IputDeleteLicensePreEdu input)
        {
            var result = await Task.Run(() => _adminService.DeleteLicensePreEducation(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddLicenseProduct([FromBody] IputAddLicenseProduct input)
        {
            var result = await Task.Run(() => _adminService.AddLicenseProduct(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLicenseProduct([FromBody] IputUpdateLicenseProduct input)
        {
            var result = await Task.Run(() => _adminService.UpdateLicenseProduct(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> DeleteLicenseProduct([FromBody] IputDeleteLicenseProduct input)
        {
            var result = await Task.Run(() => _adminService.DeleteLicenseProduct(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertLicenseTech([FromBody] IputUpsertLicenseTech input)
        {
            var result = await Task.Run(() => _adminService.UpsertLicenseTech(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> DeleteLicenseTech([FromBody] IputDeleteLicenseTech input)
        {
            var result = await Task.Run(() => _adminService.DeleteLicenseTech(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertPreEducation([FromBody] IputUpsertPreEducation input)
        {
            var result = await Task.Run(() => _adminService.UpsertPreEducation(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> DeletePreEducation([FromBody] IputDeletePreEducation input)
        {
            var result = await Task.Run(() => _adminService.DeletePreEducation(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertProduct([FromBody] IputUpsertProduct input)
        {
            var result = await Task.Run(() => _adminService.UpsertProduct(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> DeleteProduct([FromBody] IputDeleteProduct input)
        {
            var result = await Task.Run(() => _adminService.DeleteProduct(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertRequiredLicense([FromBody] IputUpsertRequiredLicense input)
        {
            var result = await Task.Run(() => _adminService.UpsertRequiredLicense(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> DeleteRequiredLicense([FromBody] IputDeleteRequiredLicense input)
        {
            var result = await Task.Run(() => _adminService.DeleteRequiredLicense(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertStateProvince([FromBody] IputUpsertStateProvince input)
        {
            var result = await Task.Run(() => _adminService.UpsertStateProvince(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> DeleteStateProvince([FromBody] IputDeleteStateProvince input)
        {
            var result = await Task.Run(() => _adminService.DeleteStateProvince(input));

            return StatusCode(result.StatusCode, result);
        }
        #endregion
    }
}
