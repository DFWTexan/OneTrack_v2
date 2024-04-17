﻿using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.DataModel;

namespace OneTrack_v2.Services
{
    public interface IEmployeeService
    {
        public Task<ReturnResult> SearchEmployee(string? vEmployeeSSN = null, string? vGEID = null, string? vSCORENumber = null,
            string? vLastName = null, string? vFirstName = null, List<string>? vAgentStatus = null, string? vResState = null, string? vWrkState = null, string? vBranchCode = null,
            int? vEmployeeLicenseID = null, List<string>? vLicStatus = null, string? vLicState = null, string? vLicenseName = null, int? vNationalProducerNumber = null);         

        //public DataModel.Response.ReturnResult GetEmployee(int vEmployeeID);
    }
}
