using DataModel.Response;

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
        public ReturnResult GetLicenseByStateProv(string vStateProv);
        public ReturnResult GetLicenseEditByID(int vLicenseID);
        public ReturnResult GetLicTechList();
        public ReturnResult GetPreEduEditByState(string vState);
        public ReturnResult GetProductEdits();
        public ReturnResult GetStateLicRequirements(string vWorkState, string vResState);
        public ReturnResult GetStateProvinceList();
        public ReturnResult GetXBorderBranchList();
        public ReturnResult GetXBorderBranchByCode(int vBranchCode);
    }
}
