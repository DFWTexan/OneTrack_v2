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
        public ReturnResult GetEmailTemplates();
        public ReturnResult GetTicklerMessageTypes();
    }
}
