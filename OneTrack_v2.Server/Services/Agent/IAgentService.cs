using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.DataModel;

namespace OneTrack_v2.Services
{
    public interface IAgentService
    {
        public ReturnResult GetAgentByEmployeeID(int vEmployeeID);
        public ReturnResult GetLicenses(int vEmploymentID);
        public ReturnResult GetAppointments(int vEmploymentID);
        public ReturnResult GetLicenseAppointments(int vEmploymentID);
        public ReturnResult GetEmploymentTransferHistory(int vEmploymentID, int vEmploymentHistoryID = 0, int vTransferHistoryID = 0, int vEmploymentJobTitleID = 0);
        public ReturnResult GetContEducationRequired(int vEmploymentID);
        public ReturnResult GetDiary(int vEmploymentID = 0);
        public ReturnResult GetCommunications(int vEmploymentID);
        public ReturnResult InsertAgent([FromBody] IputAgentInsert Input);
        public ReturnResult InsertAgent_v2([FromBody] IputAgentInsert Input);
        public ReturnResult GetLicenseApplcationInfo(int EmployeeLicenseID);
        public ReturnResult GetBranchCodes();
    }
}
