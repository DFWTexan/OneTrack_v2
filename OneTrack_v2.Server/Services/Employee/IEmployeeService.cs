using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.DataModel;

namespace OneTrack_v2.Services
{
    public interface IEmployeeService
    {
        public Task<ReturnResult> SearchEmployee(int vCompanyID, string? vEmployeeSSN, string? vGEID, string? vSCORENumber,
            int vNationalProducerNumber, string? vLastName, string? vFirstName,
            List<string>? vAgentStatus, string? vResState, string? vWrkState, string? vBranchCode,
            int vEmployeeLicenseID, string? vLicStatus, string? vLicState, string? vLicenseName, int vEmploymentID);         

        //public DataModel.Response.ReturnResult GetEmployee(int vEmployeeID);
    }
}
