using DataModel.Response;

namespace OneTrak_v2.Services
{
    public interface IDashboardService
    {
        public ReturnResult GetAuditModifiedBy();
        public ReturnResult GetAuditLog(DateTime vStartDate, DateTime vEndDate, string? vModifiedBy = null);
    }
}
