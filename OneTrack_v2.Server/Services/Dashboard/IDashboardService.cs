using DataModel.Response;

namespace OneTrak_v2.Services
{
    public interface IDashboardService
    {
        public ReturnResult GetAdBankerImportStatus();
        public ReturnResult GetAdBankerImportData(DateTime vStartDate, DateTime vEndDate, bool? vImportStatus = null);
        public ReturnResult GetAuditModifiedBy(bool vIsActive = true);
        public ReturnResult GetAuditLog(DateTime vStartDate, DateTime vEndDate, string? vModifiedBy = null);
    }
}
