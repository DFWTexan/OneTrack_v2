using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.DataModel;

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
        public ReturnResult GetPreExamByStateAbv(string vStateAbv);
        public ReturnResult GetAgentStautes();
        public ReturnResult GetAppointmentStatuses();
        public ReturnResult GetApplicationsStatuses();
        public ReturnResult GetPreEducationStatuses();
        public ReturnResult GetPreExamStatuses();
        public ReturnResult GetRenewalMethods();
        public ReturnResult GetRollOutGroups();
        public ReturnResult GetContEduInfo(string vUsageType);
    }
}
