using DataModel.Response;
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
        public ReturnResult GetAuditLogAdHoc(DateTime vStartDate, DateTime vEndDate,
                string? vModifiedBy = null, string? vBaseTableName = null,
                string? vBaseTableKeyValue = null, string? vAuditFieldName = null,
                string? vAuditAction = null, int pageNumber = 1, int pageSize = 100);
        public Task<ReturnResult> GetAuditBaseTableNames();
        public ReturnResult GetEmployeeIdWithTMemberID(string vMemberID);

        

        //public ReturnResult GetAuditLogAdHocStoredProc(DateTime vStartDate, DateTime vEndDate,
        //    string? vModifiedBy = null, string? vBaseTableName = null,
        //    string? vBaseTableKeyValue = null, string? vAuditFieldName = null,
        //    string? vAuditAction = null, int pageNumber = 1, int pageSize = 100);

        //public ReturnResult GetAuditLogAdHocCompiled(DateTime vStartDate, DateTime vEndDate,
        //    string? vModifiedBy = null, string? vBaseTableName = null,
        //    string? vBaseTableKeyValue = null, string? vAuditFieldName = null,
        //    string? vAuditAction = null, int pageNumber = 1, int pageSize = 100);
    }
}
