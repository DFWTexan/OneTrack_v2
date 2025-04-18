﻿using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using OneTrak_v2.DataModel;

namespace OneTrak_v2.Services
{
    public interface IDashboardService
    {
        public ReturnResult GetAdBankerIncompleteCount();
        public ReturnResult GetAdBankerImportStatus();
        public ReturnResult GetAdBankerImportData(DateTime vStartDate, DateTime vEndDate, bool? vImportStatus = null);
        public ReturnResult CompleteImportStatus([FromBody] IputADBankerImportStatus vInput);
        public ReturnResult GetAuditModifiedBy(bool vIsActive = true);
        public ReturnResult GetAuditLog(DateTime vStartDate, DateTime vEndDate, string? vModifiedBy = null);
        public ReturnResult GetEmployeeIdWithTMemberID(string vMemberID);
    }
}
