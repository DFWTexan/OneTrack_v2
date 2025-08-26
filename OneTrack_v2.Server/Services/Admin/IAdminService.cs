using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.DbData.Models;
using OneTrak_v2.DataModel;
using OneTrak_v2.Server.DbData.DataModel.Admin;

namespace OneTrak_v2.Services
{
    public interface IAdminService
    {
        public ReturnResult GetCompanyTypes();
        public ReturnResult GetCompaniesByType(string vCompanyType);
        public ReturnResult GetLicenseTypes(string? vStateAbv = null);
        public ReturnResult GetConEducationRules(string? vState = null, string? LicenesType = null);
        public ReturnResult GetConEducationRulesVsp(string? vState = null, string? LicenesType = null);
        public Task<ReturnResult> GetCompanyRequirementsAsync(string vWorkState, string? vResState = null);
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
        public ReturnResult UpsertCompanyRequirement([FromBody] IputUpsertCompanyRequirement vCoRequirement);
        public ReturnResult DeleteCompanyRequirement([FromBody] IputDeleteCompanyRequirement vInput);
        public ReturnResult UpsertEducationRule([FromBody] IputUpsertEducationRule vInput);
        public ReturnResult DisableEducationRule([FromBody] IputDisableEducationRule vInput);
        public ReturnResult UpsertLkpType([FromBody] IputUpsertLkpType vInput);
        public ReturnResult DeleteLkpType([FromBody] IputDeleteLkpType vInput);
        public ReturnResult UpsertExam([FromBody] IputUpsertExam vInput);
        public ReturnResult DeleteExam([FromBody] IputDeleteExam vInput);
        public ReturnResult UpsertJobTitle([FromBody] IputUpsertJobTitle vInput);
        public ReturnResult UpsertLicense([FromBody] IputUpsertLicense vInput);
        public ReturnResult DeleteLicense([FromBody] IputDeleteLicense vInput);
        public ReturnResult AddLicenseCompany([FromBody] IputAddLicenseCompany vInput);
        public ReturnResult UpdateLicenseCompany([FromBody] IputUpdateLicenseCompany vInput);
        public ReturnResult DeleteLicenseCompany([FromBody] IputDeleteLicenseCompany vInput);
        public ReturnResult AddLicenseExam([FromBody] IputAddLicenseExam vInput);
        public ReturnResult UpdateLicenseExam([FromBody] IputUpdateLicenseExam vInput);
        public ReturnResult DeleteLicenseExam([FromBody] IputDeleteLicenseExam vInput);
        public ReturnResult AddLicensePreEducation([FromBody] IputAddLicensePreEducation vInput);
        public ReturnResult UpdateLicensePreEducation([FromBody] IputUpdateLicensePreEducation vInput);
        public ReturnResult DeleteLicensePreEducation([FromBody] IputDeleteLicensePreEdu vInput);
        public ReturnResult AddLicenseProduct([FromBody] IputAddLicenseProduct vInput);
        public ReturnResult UpdateLicenseProduct([FromBody] IputUpdateLicenseProduct vInput);
        public ReturnResult DeleteLicenseProduct([FromBody] IputDeleteLicenseProduct vInput);
        public ReturnResult UpsertLicenseTech([FromBody] IputUpsertLicenseTech vInput);
        public ReturnResult DeleteLicenseTech([FromBody] IputDeleteLicenseTech vInput);
        public ReturnResult UpsertPreEducation([FromBody] IputUpsertPreEducation vInput);
        public ReturnResult DeletePreEducation([FromBody] IputDeletePreEducation vInput);
        public ReturnResult UpsertProduct([FromBody] IputUpsertProduct vInput);
        public ReturnResult DeleteProduct([FromBody] IputDeleteProduct vInput);
        public ReturnResult UpsertRequiredLicense([FromBody] IputUpsertRequiredLicense vInput);
        public ReturnResult DeleteRequiredLicense([FromBody] IputDeleteRequiredLicense vInput);
        public ReturnResult UpsertStateProvince([FromBody] IputUpsertStateProvince vInput);
        public ReturnResult DeleteStateProvince([FromBody] IputDeleteStateProvince vInput);
    }
}
