using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.DataModel;
using OneTrak_v2.DataModel;

namespace OneTrack_v2.Services
{
    public interface IAgentService
    {
        public ReturnResult GetAgentByEmployeeID(int vEmployeeID);
        //public ReturnResult GetAgentByTMemberID(int vTMemberID);
        public ReturnResult GetLicenses(int vEmploymentID);
        public ReturnResult GetAppointments(int vEmploymentID);
        public ReturnResult GetLicenseAppointments(int vEmploymentID);
        public ReturnResult GetEmploymentTransferHistory(int vEmploymentID, int vEmploymentHistoryID = 0, int vTransferHistoryID = 0, int vEmploymentJobTitleID = 0);
        public ReturnResult GetContEducationRequired(int vEmploymentID);
        public ReturnResult GetDiary(int vEmploymentID = 0);
        public ReturnResult GetCommunications(int vEmploymentID);
        public ReturnResult GetLicenseApplcationInfo(int vEmployeeLicenseID);
        public ReturnResult GetBranchCodes();
        public ReturnResult GetCoRequirementAssetIDs();
        public ReturnResult GetCoRequirementStatuses();
        public ReturnResult GetLicLevels();
        public ReturnResult GetLicIncentives();
        public ReturnResult UpsertAgent([FromBody] IputUpsertAgent vInput);
        public ReturnResult InsertAgent_v2([FromBody] IputUpsertAgent vInput);
        public ReturnResult DeleteAgentEmployee(int vEmployeeID, string vUserSOEID);
        public ReturnResult UpdateAgentDetails([FromBody] IputAgentDetail vInput);
        public ReturnResult UpdateAgentNatNumber([FromBody] IputAgentDetail vInput);
        public ReturnResult UpsertEmploymentHistItem([FromBody] InputEmploymentHistItem vInput);
        public ReturnResult DeleteEmploymentHistItem([FromBody] IputDeleteEmploymentHistoryItem vInput);
        public ReturnResult UpsertTranserHistItem([FromBody] IputTransferHistoryItem vInput);
        public ReturnResult DeleteTransferHistItem([FromBody] IputDeleteTransferHisttoryItem vInput);
        public ReturnResult UpsertCoRequirementItem([FromBody] IputCoRequirementItem vInput);
        public ReturnResult DeleteCoRequirementItem([FromBody] IputDeleteCoRequirementItem vInput);
        public ReturnResult UpsertEmploymentJobTitleItem([FromBody] IputEmploymentJobTitleItem vInput);
        public ReturnResult DeleteEmploymentJobTitleItem([FromBody] IputDeleteEmploymentJobTitle vInput);
        public ReturnResult UpsertAgentLicense([FromBody] IputUpsertAgentLicense vInput);
        public ReturnResult DeleteAgentLicense([FromBody] IputDeleteAgentLincense vInput);
        public ReturnResult UpsertConEduTaken([FromBody] IputUpsertConEduTaken vInput);
        public ReturnResult DeleteConEduTaken([FromBody] IputDeleteConEduTaken vInput);
        public ReturnResult UpsertDiaryItem([FromBody] IputUpsertDiaryItem vInput);
        public ReturnResult DeleteDiaryItem([FromBody] IputDeleteDiaryItem vInput);
        public ReturnResult CloseWorklistItem([FromBody] IputAgentWorklistItem vInput);
    }
}
