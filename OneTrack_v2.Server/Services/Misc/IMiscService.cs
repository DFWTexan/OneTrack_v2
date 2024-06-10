using DataModel.Response;

namespace OneTrack_v2.Services
{
    public interface IMiscService
    {
        public ReturnResult GetStateProvinces();
        public ReturnResult GetBranches();
        public ReturnResult GetScoreNumbers();
        public ReturnResult GetEmployerAgencies();
        public ReturnResult GetLicenseStatuses();
        public ReturnResult GetLicenseNames();
        public ReturnResult GetLicenseNumericNames(string vStateAbv);
        public ReturnResult GetEmailTemplates();
        public ReturnResult GetTicklerMessageTypes();
        public ReturnResult WorkListNames();
        public ReturnResult GetLicenseTeches();
        public ReturnResult GetBackgroundStatuses();
        public ReturnResult GetJobTitles();
        public ReturnResult GetCoAbvByLicenseID(int vLicenseID);
        public ReturnResult GetPreEducationByStateAbv(string vStateAbv);
    }
}
