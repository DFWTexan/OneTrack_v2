﻿using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using OneTrack_v2.DataModel;
using OneTrack_v2.Services;
using OneTrak_v2.DataModel;
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
        #endregion
    }
}
