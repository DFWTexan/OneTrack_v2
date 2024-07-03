﻿using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.DbData.Models;
using OneTrak_v2.DataModel;

namespace OneTrak_v2.Services
{
    public interface IAdminService
    {
        public ReturnResult GetCompanyTypes();
        public ReturnResult GetCompaniesByType(string vCompanyType);
        public ReturnResult GetLicenseTypes(string? vStateAbv = null);
        public ReturnResult GetConEducationRules(string? vState = null, string? LicenesType = null);
        public ReturnResult GetCompanyRequirements(string vWorkState, string? vResState = null);
        public ReturnResult GetDropdownListTypes();
        public ReturnResult GetDropdownByType(string vLkpField);
        public ReturnResult GetExamByState(string vState);
        public ReturnResult GetJobTitleLicLevel();
        public ReturnResult GetJobTitlelicIncentive();
        public ReturnResult GetJobTitles();
        public ReturnResult GetLicenseByStateProv(string? vStateProv = null);
        public ReturnResult GetLicenseEditByID(int vLicenseID);
        public ReturnResult GetLicTechList();
        public ReturnResult GetPreEduEditByState(string vState);
        public ReturnResult GetProductEdits();
        public ReturnResult GetStateLicRequirements(string? vWorkState = null, string? vResState = null, string? vBranchCode = null);
        public ReturnResult GetStateProvinceList();
        public ReturnResult GetXBorderBranchCodes();
        public ReturnResult GetXBorLicRequirements(string vBranchCode);
        public ReturnResult UpsertCompany([FromBody] IputUpsertCompany company);
        public ReturnResult DeleteCompany([FromBody] IputDeleteCompany vInput);
    }
}
