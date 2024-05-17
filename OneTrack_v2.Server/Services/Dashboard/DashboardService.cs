using DataModel.Response;
using OneTrack_v2.DbData;
using OneTrack_v2.DbData.Models;

namespace OneTrak_v2.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDataContext _db;

        public DashboardService(AppDataContext db) { _db = db;}

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
                result.ErrMessage = ex.Message;
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
                result.ErrMessage = ex.Message;
            }

            return result;
        }

    }
}
