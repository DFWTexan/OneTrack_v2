﻿using Microsoft.AspNetCore.Mvc;
using OneTrak_v2.DataModel;
using OneTrak_v2.Services;

namespace OneTrak_v2.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LicenseInfoController : ControllerBase
    {
        private readonly ILicenseInfoService _licenseInfo;

        public LicenseInfoController(ILicenseInfoService licenseInfo)
        {
            _licenseInfo = licenseInfo;
        }

        [HttpGet("{employeelicenseID}")]
        public async Task<ActionResult> GetIncentiveInfo(int employeelicenseID)
        {
            var result = await Task.Run(() => _licenseInfo.GetIncentiveInfo(employeelicenseID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetIncentiveRolloutGroups()
        {
            var result = await Task.Run(() => _licenseInfo.GetIncentiveRolloutGroups());

            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet]
        public async Task<ActionResult> GetIncentiveBMMgrs()
        {
            var result = await Task.Run(() => _licenseInfo.GetIncentiveBMMgrs());

            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet]
        public async Task<ActionResult> GetIncentiveDMMrgs()
        {
            var result = await Task.Run(() => _licenseInfo.GetIncentiveDMMrgs());

            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet]
        public async Task<ActionResult> GetIncentiveTechNames()
        {
            var result = await Task.Run(() => _licenseInfo.GetIncentiveTechNames());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAffiliatedLicenses(string stateProvinceAbv, int licenseID)
        {
            var result = await Task.Run(() => _licenseInfo.GetAffiliatedLicenses(stateProvinceAbv, licenseID));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> AddLicenseAppointment([FromBody] IputAddLicenseAppointment input)
        {
            var result = await Task.Run(() => _licenseInfo.AddLicenseAppointment(input));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateLicenseAppointment([FromBody] IputUpdateLicenseAppointment input)
        {
            var result = await Task.Run(() => _licenseInfo.UpdateLicenseAppointment(input));

            return StatusCode(result.StatusCode, result);
        }
    }
}
