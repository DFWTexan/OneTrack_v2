using DataModel.Response;
using OneTrack_v2.DbData;
using OneTrack_v2.DbData.Models;
using OneTrack_v2.Services;

namespace OneTrak_v2.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDataContext _db;
        private readonly IUtilityHelpService _utilityService;

        public DashboardService(AppDataContext db, IUtilityHelpService utilityHelpService) { _db = db; _utilityService = utilityHelpService; }

        public ReturnResult GetAdBankerIncompleteCount()
        {

            var result = new ReturnResult();
            try
            {
                var incompleteCount = _db.StgADBankerImports.Count(x => x.IsImportComplete == false);
                result.ObjData = incompleteCount;
                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# DASH-8807-12347].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }
            return result;
        }
        public ReturnResult GetAdBankerImportStatus()
        {
            var result = new ReturnResult();
            try
            {
                var maxCreateDate = _db.StgADBankerImports.Max(x => x.CreateDate.Date);
                var count = _db.StgADBankerImports.Count(x => x.CreateDate.Date == maxCreateDate);

                var importStatus = new { RecordCount = count, LastImportDate = maxCreateDate };

                result.ObjData = importStatus;
                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# DASH-8807-32007].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }
        public ReturnResult GetAdBankerImportData(DateTime vStartDate, DateTime vEndDate, bool? vImportStatus = null)
        {
            var result = new ReturnResult();
            try
            {
                var query = _db.StgADBankerImports.AsQueryable();

                query = query.Where(x => x.CreateDate.Date >= vStartDate.Date && x.CreateDate.Date <= vEndDate.Date);

                if (vImportStatus.HasValue)
                {
                    query = query.Where(x => x.IsImportComplete == vImportStatus);
                }

                var adBankerImportData = query.Select(x => new
                {
                    x.TeamMemberId,
                    x.CourseState,
                    x.StudentName,
                    x.CourseTitle,
                    x.CompletionDate,
                    x.ReportedDate,
                    x.TotalCredits,
                    x.CreateDate,
                    x.IsImportComplete
                }).OrderBy(x => x.IsImportComplete) // false first
                  .ThenByDescending(x => x.CreateDate).ToList();

                result.ObjData = adBankerImportData;
                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# DASH-8807-12347].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }
        public ReturnResult GetAuditModifiedBy(bool vIsActive = true)
        {
            var result = new ReturnResult();
            try
            {
                if (vIsActive)
                {
                    var modifiedBy = _db.AuditLogs
                        .Join(
                            _db.LicenseTeches,
                            auditLog => auditLog.ModifiedBy,
                            licenseTech => licenseTech.Soeid,
                            (auditLog, licenseTech) => auditLog.ModifiedBy
                        )
                        .Distinct()
                        .OrderBy(x => x)
                        .ToList();

                    result.ObjData = modifiedBy;
                }
                else
                {
                    var modifiedBy = _db.AuditLogs
                        .Select(x => x.ModifiedBy)
                        .Distinct()
                        .OrderBy(x => x)
                        .ToList();

                    result.ObjData = modifiedBy;
                }

                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# DASH-8807-12007].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }
        public ReturnResult GetAuditLog(DateTime vStartDate, DateTime vEndDate, string? vModifiedBy = null)
        {
            var result = new ReturnResult();
            try
            {
                var query = _db.AuditLogs.AsQueryable();

                query = query.Where(x => x.ModifyDate >= vStartDate && x.ModifyDate <= vEndDate);

                if (!string.IsNullOrEmpty(vModifiedBy))
                {
                    query = query.Where(x => x.ModifiedBy != null && x.ModifiedBy == vModifiedBy);
                }

                var auditLog = query.Select(x => new AuditLog
                {
                    ModifyDate = x.ModifyDate,
                    ModifiedBy = x.ModifiedBy,
                    AuditFieldName = x.AuditFieldName,
                    AuditAction = x.AuditAction,
                    AuditValueBefore = x.AuditValueBefore,
                    AuditValueAfter = x.AuditValueAfter
                }).OrderByDescending(x => x.ModifyDate).ToList();

                result.ObjData = auditLog;
                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# DASH-8807-12099].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }

    }
}
